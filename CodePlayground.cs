using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;
using SnipIt.Managers;
using SnipIt.Models;
using static SnipIt.timer;

namespace SnipIt
{
    public partial class CodePlayground : Form
    {
        // disclaimer really messy
        // still retains some parts from previous iterations that i didnt get rid of for future reference
        // not necessarily everything has been used as they will serve as future reference


        private int borderSize = 2;
        private Size formSize;
        Color color1 = ColorTranslator.FromHtml("#272757");
        Color color2 = ColorTranslator.FromHtml("#8686AC");
        Color color3 = ColorTranslator.FromHtml("#505081");
        Color color4 = ColorTranslator.FromHtml("#0F0E47");

        private Scintilla? scintillaEditor;
        private Scintilla? scintillaOutput;

        public CodePlayground()
        {
            InitializeComponent();
            InitializeScintillaControls();

            pnlTitlebar.BackColor = color4;
            pnlSettings.BackColor = color1;

            btnCopy.BackColor = color1;
            btnSave.BackColor = color1;
            btnRun.BackColor = color1;

            CodePlayground_Resize(this, EventArgs.Empty);
            SetLanguage("c", btnC);
        }

        public Dashboard codeplayground
        {
            get => default;
            set
            {
            }
        }

        internal Dashboard Dashboard
        {
            get => default;
            set
            {
            }
        }

        private void CodePlayground_Load(object sender, EventArgs e)
        {

        }

        private void InitializeScintillaControls()
        {
            // scintilla thingies
            scintillaEditor = new Scintilla
            {
                Dock = DockStyle.Fill,
                Lexer = Lexer.Cpp
            };

            scintillaOutput = new Scintilla
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Lexer = Lexer.Null
            };

            scintillaOutput.Styles[Style.Default].Font = "Consolas";
            scintillaOutput.Styles[Style.Default].Size = 12;
            scintillaOutput.Styles[Style.Default].ForeColor = Color.Black;
            scintillaOutput.Styles[Style.Default].BackColor = Color.White;
            scintillaOutput.StyleClearAll();

            splitContainer1.Panel1.Controls.Clear();
            splitContainer1.Panel1.Controls.Add(scintillaEditor);

            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(scintillaOutput);
        }

        private void RunCode()
        {
            if (timer.SessionManager.UserId <= 0)
            {
                DisplayOutput("Please log in to run code.");
                return;
            }

            if (scintillaEditor == null || string.IsNullOrWhiteSpace(scintillaEditor.Text))
            {
                DisplayOutput("No code to execute!");
                return;
            }

            try
            {
                DisplayOutput("Executing code...");
                string language = GetSelectedLanguage();
                string code = scintillaEditor.Text;
                string output = ExecuteCodeByLanguage(language, code);

                DisplayOutput(output); // display the output


                string snippetId = null;
                if (scintillaEditor.Tag != null)
                {
                    // get snippet id
                    if (scintillaEditor.Tag is string tagStr)
                    {
                        snippetId = tagStr;
                    }
                    else if (scintillaEditor.Tag is Dictionary<string, object> metadata)
                    {
                        if (metadata.TryGetValue("Id", out object id) && id != null)
                        {
                            snippetId = id.ToString();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(snippetId))
                {
                    string userId = timer.SessionManager.UserId.ToString();
                    string errorLog = output.Contains("Error:") ? output : null;

                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Execution Error: {ex.Message}\n{ex.StackTrace}";
                DisplayOutput(errorMessage);

                string snippetId = null;
                if (scintillaEditor.Tag != null && scintillaEditor.Tag is string)
                {
                    snippetId = (string)scintillaEditor.Tag;
                    string userId = timer.SessionManager.UserId.ToString();

                }
            }
        }

        private string ExecuteCodeByLanguage(string language, string code)
        {
            switch (language.ToLower())
            {
                case "python":
                    return ExecutePython(code);
                case "c":
                    return CompileAndRunC(code);
                case "cpp":
                    return CompileAndRunCPP(code);
                default:
                    return $"Unsupported language: {language}";
            }
        }

        private void DisplayOutput(string text)
        {
            if (scintillaOutput == null) return;

            // ensure ui update thingy
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => DisplayOutput(text)));
                return;
            }

            scintillaOutput.ReadOnly = false;
            scintillaOutput.Text = text;
            scintillaOutput.ReadOnly = true;
        }

        private string ExecutePython(string code)
        {
            // temporary directory for internal execution
            string tempDir = Path.Combine(Path.GetTempPath(), $"snipit_python_{Guid.NewGuid()}");
            string tempFile = Path.Combine(tempDir, "program.py");
            string inputFile = null;
            string outputFile = Path.Combine(tempDir, "output.txt");
            string errorFile = Path.Combine(tempDir, "error.txt");

            try
            {
                Directory.CreateDirectory(tempDir);
                if (code.Contains("input("))
                {
                    List<string> inputPrompts = ExtractPythonInputPrompts(code);
                    string userInput = PromptForUserInput(inputPrompts);
                    if (userInput == null)
                        return "Execution cancelled: User input required but not provided.";

                    // input file
                    inputFile = Path.Combine(tempDir, "input.txt");
                    File.WriteAllText(inputFile, userInput);
                }

                File.WriteAllText(tempFile, code);

                var psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"\"{tempFile}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = tempDir // set working directory to our temp folder
                };

                // execute with proper timeout and input handling sa python
                using var process = new Process { StartInfo = psi };
                var outputBuilder = new StringBuilder();
                var errorBuilder = new StringBuilder();

                process.OutputDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                    {
                        outputBuilder.AppendLine(e.Data);
                        File.AppendAllText(outputFile, e.Data + Environment.NewLine);
                    }
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                    {
                        errorBuilder.AppendLine(e.Data);
                        File.AppendAllText(errorFile, e.Data + Environment.NewLine);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // feed input file line by line
                if (inputFile != null)
                {
                    string[] inputLines = File.ReadAllLines(inputFile);
                    foreach (string line in inputLines)
                    {
                        process.StandardInput.WriteLine(line);
                    }
                }
                // close input stream
                process.StandardInput.Close();
                // Wait for process with increased timeout (30 seconds) so that it doesnt get too heavy or load too long ive been traumatized
                if (!process.WaitForExit(30000))
                {
                    try { process.Kill(); } catch { /* Process might have exited already */ }
                    return "Process timed out after 30 seconds. Your code may have an infinite loop or is waiting for additional input.";
                }

                // Check for errors
                string result = outputBuilder.ToString().Trim();
                string error = errorBuilder.ToString().Trim();

                // Keep output files for debugging (only delete directory on successful runs) tho not really used in my overall app
                if (process.ExitCode != 0 || !string.IsNullOrEmpty(error))
                {
                    return $"Exit code: {process.ExitCode}\n\nOutput:\n{result}\n\nErrors/Warnings:\n{error}\n\nTemporary files saved at: {tempDir}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return $"Python execution failed: {ex.Message}\nStack trace: {ex.StackTrace}";
            }
            finally
            {
                bool keepTempFiles = false; // delete temp files

                if (!keepTempFiles)
                {
                    TryDeleteDirectory(tempDir);
                }
            }
        }

        private List<string> ExtractPythonInputPrompts(string code)
        {
            List<string> prompts = new List<string>();
            int index = 0;
            while ((index = code.IndexOf("input(", index)) >= 0)
            {
                // closing parenthesissssss
                int openingIndex = index + 6; // Skip "input("
                int closingIndex = FindMatchingClosingParenthesis(code, openingIndex);

                if (closingIndex > openingIndex)
                {
                    string promptText = code.Substring(openingIndex, closingIndex - openingIndex).Trim();

                    // extract the string content if it's a string literal
                    if ((promptText.StartsWith("\"") && promptText.EndsWith("\"")) ||
                        (promptText.StartsWith("'") && promptText.EndsWith("'")))
                    {
                        promptText = promptText.Substring(1, promptText.Length - 2);
                        prompts.Add(promptText);
                    }
                    // handle f-strings, triple quotes, etc.
                    else if (promptText.Contains("\"") || promptText.Contains("'"))
                    {
                        // simple f-string handling
                        int firstQuote = promptText.IndexOfAny(new[] { '\"', '\'' });
                        if (firstQuote >= 0)
                        {
                            char quoteChar = promptText[firstQuote];
                            int secondQuote = promptText.IndexOf(quoteChar, firstQuote + 1);
                            if (secondQuote > firstQuote)
                            {
                                promptText = promptText.Substring(firstQuote + 1, secondQuote - firstQuote - 1);
                                prompts.Add(promptText);
                            }
                        }
                    }
                }

                index = closingIndex + 1;
            }

            return prompts;
        }

        private int FindMatchingClosingParenthesis(string text, int openPos)
        {
            int depth = 1;
            bool inSingleQuote = false;
            bool inDoubleQuote = false;

            for (int i = openPos; i < text.Length; i++)
            {
                char c = text[i];

                // handle string literals so we don't match parentheses inside strings
                if (c == '\"' && !inSingleQuote)
                    inDoubleQuote = !inDoubleQuote;
                else if (c == '\'' && !inDoubleQuote)
                    inSingleQuote = !inSingleQuote;

                // only count parentheses outside of strings
                if (!inSingleQuote && !inDoubleQuote)
                {
                    if (c == '(')
                        depth++;
                    else if (c == ')')
                    {
                        depth--;
                        if (depth == 0)
                            return i;
                    }
                }
            }

            return -1; // No matching parenthesis found
        }

        private string CompileAndRunC(string code)
        {
            string tempDir = Path.Combine(Path.GetTempPath(), $"snipit_c_{Guid.NewGuid()}");
            string sourceFile = Path.Combine(tempDir, "program.c");
            string exeFile = Path.Combine(tempDir, "program.exe");
            string inputFile = null;
            string objDir = Path.Combine(tempDir, "obj");
            string outputFile = Path.Combine(tempDir, "output.txt");
            string errorFile = Path.Combine(tempDir, "error.txt");

            try
            {
                // Create temporary directories
                Directory.CreateDirectory(tempDir);
                Directory.CreateDirectory(objDir);

                // verify if nay user input and prep input file
                bool hasInputFunction = code.Contains("scanf(") ||
                                       code.Contains("gets(") ||
                                       code.Contains("fgets(") ||
                                       code.Contains("getchar(") ||
                                       code.Contains("getc(") ||
                                       code.Contains("read(");

                List<string> inputPrompts = new List<string>();

                // Try to extract prompts from printf before scanf
                if (hasInputFunction)
                {
                    inputPrompts = ExtractCInputPrompts(code);

                    // user input
                    string userInput = PromptForUserInput(inputPrompts);
                    if (userInput == null)
                        return "Execution cancelled: User input required but not provided.";

                    // input file
                    inputFile = Path.Combine(tempDir, "input.txt");
                    File.WriteAllText(inputFile, userInput);
                }

                // Write code to file; CLYDE DO NOT TOUCH
                File.WriteAllText(sourceFile, code);
                string includePaths = "";
                string compileCommand = $"\"{sourceFile}\" -o \"{exeFile}\" -Wall -Wextra -pedantic -std=c11 {includePaths} -fdiagnostics-color=never";
                compileCommand += " -g";
                string compileOutput = ExecuteCommand("gcc", compileCommand, 30000); // 30 sec timeout 

                if (!string.IsNullOrEmpty(compileOutput))
                {
                    return $"Compilation Error:\n{compileOutput}\n\nSource code preserved at: {sourceFile}";
                }

                if (!File.Exists(exeFile))
                {
                    return "Compilation failed: No executable was generated.";
                }

                string executionResult;
                if (inputFile != null)
                {
                    executionResult = ExecuteProgramWithInput(exeFile, tempDir, inputFile, outputFile, errorFile);
                }
                else
                {
                    executionResult = ExecuteProgram(exeFile, tempDir, outputFile, errorFile);
                }

                return executionResult;
            }
            catch (Exception ex)
            {
                return $"C compilation/execution failed: {ex.Message}\nStack trace: {ex.StackTrace}";
            }
            finally
            {
                bool keepTempFiles = false; 

                if (!keepTempFiles)
                {
                    TryDeleteDirectory(tempDir);
                }
            }
        }


        private string CompileAndRunCPP(string code)
        {
            string tempDir = Path.Combine(Path.GetTempPath(), $"snipit_cpp_{Guid.NewGuid()}");
            string sourceFile = Path.Combine(tempDir, "program.cpp");
            string exeFile = Path.Combine(tempDir, "program.exe");
            string inputFile = null;
            string objDir = Path.Combine(tempDir, "obj");
            string outputFile = Path.Combine(tempDir, "output.txt");
            string errorFile = Path.Combine(tempDir, "error.txt");

            try
            {
                Directory.CreateDirectory(tempDir);
                Directory.CreateDirectory(objDir);

                // verify if nay user input and prep input file
                bool hasInputOperation = code.Contains("cin >>") ||
                                        code.Contains("std::cin") ||
                                        code.Contains("scanf(") ||
                                        code.Contains("gets(") ||
                                        code.Contains("getline(") ||
                                        code.Contains("std::getline");

                List<string> inputPrompts = new List<string>();

                if (hasInputOperation)
                {
                    inputPrompts = ExtractCppInputPrompts(code);

                    // user input
                    string userInput = PromptForUserInput(inputPrompts);
                    if (userInput == null)
                        return "Execution cancelled: User input required but not provided.";

                    // input file
                    inputFile = Path.Combine(tempDir, "input.txt");
                    File.WriteAllText(inputFile, userInput);
                }

                // Write code to file
                File.WriteAllText(sourceFile, code);
                string includePaths = "";
                string compileCommand = $"\"{sourceFile}\" -o \"{exeFile}\" -Wall -Wextra -pedantic -std=c++17 {includePaths} -fdiagnostics-color=never";
                compileCommand += " -O2";
                compileCommand += " -g";
                string compileOutput = ExecuteCommand("g++", compileCommand, 30000); // 30 sec timeout

                if (!string.IsNullOrEmpty(compileOutput))
                {
                    return $"Compilation Error:\n{compileOutput}\n\nSource code preserved at: {sourceFile}";
                }

                if (!File.Exists(exeFile))
                {
                    return "Compilation failed: No executable was generated.";
                }

                string executionResult;
                if (inputFile != null)
                {
                    executionResult = ExecuteProgramWithInput(exeFile, tempDir, inputFile, outputFile, errorFile);
                }
                else
                {
                    executionResult = ExecuteProgram(exeFile, tempDir, outputFile, errorFile);
                }

                return executionResult;
            }
            catch (Exception ex)
            {
                return $"C++ compilation/execution failed: {ex.Message}\nStack trace: {ex.StackTrace}";
            }
            finally
            {
                bool keepTempFiles = false;

                if (!keepTempFiles)
                {
                    TryDeleteDirectory(tempDir);
                }
            }
        }

        private string ExecuteProgram(string exePath, string workingDir, string outputFile, string errorFile)
        {
            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = workingDir
            };

            try
            {
                using var process = new Process { StartInfo = psi };
                var outputBuilder = new StringBuilder();
                var errorBuilder = new StringBuilder();

                process.OutputDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                    {
                        outputBuilder.AppendLine(e.Data);
                        File.AppendAllText(outputFile, e.Data + Environment.NewLine);
                    }
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                    {
                        errorBuilder.AppendLine(e.Data);
                        File.AppendAllText(errorFile, e.Data + Environment.NewLine);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // 30 sec timeout
                if (!process.WaitForExit(30000))
                {
                    try { process.Kill(); } catch { /* Process might have exited already */ }
                    return "Process timed out after 30 seconds. Your code may have an infinite loop or is waiting for input.";
                }

                string result = outputBuilder.ToString().Trim();
                string error = errorBuilder.ToString().Trim();

                if (process.ExitCode != 0 || !string.IsNullOrEmpty(error))
                {
                    return $"Exit code: {process.ExitCode}\n\nOutput:\n{result}\n\nErrors/Warnings:\n{error}\n\nExecutable preserved at: {exePath}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return $"Program execution failed: {ex.Message}";
            }
        }

        private string ExecuteProgramWithInput(string exePath, string workingDir, string inputFile, string outputFile, string errorFile)
        {
            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                WorkingDirectory = workingDir
            };

            try
            {
                using var process = new Process { StartInfo = psi };
                var outputBuilder = new StringBuilder();
                var errorBuilder = new StringBuilder();

                process.OutputDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                    {
                        outputBuilder.AppendLine(e.Data);
                        File.AppendAllText(outputFile, e.Data + Environment.NewLine);
                    }
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                    {
                        errorBuilder.AppendLine(e.Data);
                        File.AppendAllText(errorFile, e.Data + Environment.NewLine);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // Read input file and feed it line by line to avoid buffer issues
                if (File.Exists(inputFile))
                {
                    string[] inputLines = File.ReadAllLines(inputFile);
                    foreach (string line in inputLines)
                    {
                        process.StandardInput.WriteLine(line);
                    }
                }

                process.StandardInput.Close();

                if (!process.WaitForExit(30000))
                {
                    try { process.Kill(); } catch { /* Process might have exited already */ }
                    return "Process timed out after 30 seconds. Your code may have an infinite loop or needs more input than provided.";
                }

                string result = outputBuilder.ToString().Trim();
                string error = errorBuilder.ToString().Trim();

                if (process.ExitCode != 0 || !string.IsNullOrEmpty(error))
                {
                    return $"Exit code: {process.ExitCode}\n\nOutput:\n{result}\n\nErrors/Warnings:\n{error}\n\nExecutable preserved at: {exePath}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return $"Program execution failed: {ex.Message}";
            }
        }

        private List<string> ExtractCInputPrompts(string code)
        {
            List<string> prompts = new List<string>();

            // Find printf statements before scanf
            int index = 0;
            while (true)
            {
                // next scanf position
                int scanfPos = code.IndexOf("scanf(", index);
                if (scanfPos < 0) break; // No more scanf calls

                // find printf before this scanf
                int printfPos = code.LastIndexOf("printf(", scanfPos);
                if (printfPos >= index && printfPos < scanfPos)
                {
                    // Extract the printf argument
                    int openParenPos = printfPos + 7; // Skip "printf("
                    int closeParenPos = FindMatchingClosingParenthesis(code, openParenPos);

                    if (closeParenPos > openParenPos)
                    {
                        string printfArgs = code.Substring(openParenPos, closeParenPos - openParenPos);

                        // Extract string literal if present
                        if (printfArgs.Contains("\""))
                        {
                            int firstQuote = printfArgs.IndexOf("\"");
                            int secondQuote = printfArgs.IndexOf("\"", firstQuote + 1);

                            if (secondQuote > firstQuote)
                            {
                                string prompt = printfArgs.Substring(firstQuote + 1, secondQuote - firstQuote - 1);
                                prompts.Add(prompt);
                            }
                        }
                    }
                }

                index = scanfPos + 6;
            }

            return prompts;
        }

        private List<string> ExtractCppInputPrompts(string code)
        {
            List<string> prompts = new List<string>();

            // Find cout statements before cin
            int index = 0;
            while (true)
            {
                // Find next cin position
                int cinPos = code.IndexOf("cin", index);
                if (cinPos < 0) break; // No more cin operations

                // ensure if a proper cin operation ba or di (not part of another word)
                if (cinPos > 0 && char.IsLetterOrDigit(code[cinPos - 1]))
                {
                    index = cinPos + 3;
                    continue;
                }

                // Look for cout before this cin
                int coutPos = code.LastIndexOf("cout", cinPos);
                if (coutPos >= index && coutPos < cinPos)
                {
                    // Find the << after cout
                    int insertPos = code.IndexOf("<<", coutPos);
                    if (insertPos > coutPos && insertPos < cinPos)
                    {
                        // Find the semicolon that ends this cout statement
                        int semicolonPos = code.IndexOf(";", insertPos);
                        if (semicolonPos > insertPos && semicolonPos < cinPos)
                        {
                            string coutStatement = code.Substring(insertPos, semicolonPos - insertPos);

                            // Extract string literals
                            int firstQuote = coutStatement.IndexOf("\"");
                            if (firstQuote >= 0)
                            {
                                int secondQuote = coutStatement.IndexOf("\"", firstQuote + 1);
                                if (secondQuote > firstQuote)
                                {
                                    string prompt = coutStatement.Substring(firstQuote + 1, secondQuote - firstQuote - 1);
                                    prompts.Add(prompt);
                                }
                            }
                        }
                    }
                }

                index = cinPos + 3;
            }

            prompts.AddRange(ExtractCInputPrompts(code));

            return prompts;
        }

        private string ExecuteCommand(string exe, string arguments, int timeoutMs = 30000)
        {
            var psi = new ProcessStartInfo
            {
                FileName = exe,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using var process = new Process { StartInfo = psi };

                var outputBuilder = new StringBuilder();
                var errorBuilder = new StringBuilder();

                process.OutputDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                        outputBuilder.AppendLine(e.Data);
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                        errorBuilder.AppendLine(e.Data);
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.StandardInput.Close();

                if (!process.WaitForExit(timeoutMs))
                {
                    try
                    {
                        process.Kill();
                    }
                    catch
                    {
                    }
                    return "Process timed out. This may be caused by code waiting for user input.";
                }

                if (process.ExitCode != 0 && errorBuilder.Length > 0)
                {
                    return $"Error (Exit code: {process.ExitCode}):\n{errorBuilder}";
                }

                // return combined output
                string result = outputBuilder.ToString().Trim();
                string error = errorBuilder.ToString().Trim();

                if (!string.IsNullOrEmpty(error))
                {
                    return $"{result}\n\nWarnings/Errors:\n{error}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return $"Command execution failed: {ex.Message}";
            }
        }

        private void TryDeleteFile(string path)
        {
            try
            {
                if (path != null && File.Exists(path))
                    File.Delete(path);
            }
            catch (Exception)
            {

            }
        }

        private void TryDeleteDirectory(string path)
        {
            try
            {
                if (path != null && Directory.Exists(path))
                    Directory.Delete(path, true);
            }
            catch (Exception)
            {

            }
        }

        private string GetSelectedLanguage()
        {
            if (scintillaEditor == null)
                return "python";

            // Check which language button is highlighted
            if (btnC != null && btnC.BackColor == Color.LightBlue)
                return "c";
            if (btnCPP != null && btnCPP.BackColor == Color.LightBlue)
                return "cpp";
            if (btnPython != null && btnPython.BackColor == Color.LightBlue)
                return "python";

            // if no button is highlighted, try to use the tag pero just for ensuring ra ni
            return scintillaEditor.Tag?.ToString() ?? "python"; // python as default if ever null but it should work just fine
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            RunCode();
        }

        private void SetLanguage(string language, Button selectedButton)
        {
            btnC.BackColor = color1;
            btnCPP.BackColor = color1;
            btnPython.BackColor = color1;

            selectedButton.BackColor = Color.LightBlue;
            ConfigureScintilla(scintillaEditor, language);
            scintillaEditor.Tag = language;

            scintillaEditor.Text = GetHelloWorldSnippet(language); // do not get rid of it yet but may not be in the final output keep this for the future
        }


        private void ConfigureScintilla(Scintilla scintilla, string language)
        {
            if (scintilla == null) return;

            scintilla.Lexer = Lexer.Null;

            switch (language.ToLower())
            {
                case "python":
                    scintilla.Lexer = Lexer.Python;
                    break;
                case "c":
                    scintilla.Lexer = Lexer.Cpp;
                    break;
                case "cpp":
                    scintilla.Lexer = Lexer.Cpp;
                    break;
            }

            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 12;
            scintilla.SetSelectionBackColor(true, Color.LightGray);
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            SetLanguage("c", btnC);
        }

        private void btnCPP_Click(object sender, EventArgs e)
        {
            SetLanguage("cpp", btnCPP);
        }

        private void btnPython_Click(object sender, EventArgs e)
        {
            SetLanguage("python", btnPython);
        }

        private void CodePlayground_Resize(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated || splitContainer1 == null)
            {
                return;
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                return;
            }

            if (splitContainer1.Width > 10)
            {
                splitContainer1.SplitterDistance = Math.Max(10, splitContainer1.Width / 2); 
            }
        }

        private string GetHelloWorldSnippet(string language)
        {
            switch (language.ToLower())
            {
                case "python":
                    return "# Python Compiler\n #Write your code here\nprint(\"Hello, World!\")";
                case "c":
                    return "// C Compiler\n#include <stdio.h>\n\nint main() {\n// Write your code here\n    printf(\"Hello, World!\\n\");\n    return 0;\n}";
                case "cpp":
                    return "// C++/Cpp Compiler\n#include <iostream>\n\nint main() {\n// Write your code here\n    std::cout << \"Hello, World!\" << std::endl;\n    return 0;\n}";
                default:
                    return "";
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ptbBack_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (timer.SessionManager.UserId <= 0)
            {
                MessageBox.Show("Please log in to save snippets.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (scintillaEditor == null || string.IsNullOrWhiteSpace(scintillaEditor.Text))
            {
                MessageBox.Show("No code to save!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string language = GetSelectedLanguage();
            string userId = timer.SessionManager.UserId.ToString();

            bool isEditing = false;
            string existingId = null;
            string existingName = null;
            string existingCodeType = null;

            if (scintillaEditor.Tag != null && scintillaEditor.Tag is string snippetId)
            {
                // try loading existing snippets
                var allSnippets = SnippetManager.LoadAllSnippets();
                var existingSnippet = allSnippets.Find(s => s.Id == snippetId);

                if (existingSnippet != null)
                {
                    isEditing = true;
                    existingId = existingSnippet.Id;
                    existingName = existingSnippet.Name;
                    existingCodeType = existingSnippet.CodeType;
                }
            }

            var (snippetName, codeType) = PromptForSnippetName(isEditing, existingName, existingCodeType);
            if (string.IsNullOrWhiteSpace(snippetName))
                return;

            Snippet snippet;
            if (isEditing && existingId != null)
            {
                // Update existing snippet
                var allSnippets = SnippetManager.LoadAllSnippets();
                snippet = allSnippets.Find(s => s.Id == existingId);

                if (snippet != null)
                {
                    // Update existing snippet
                    snippet.Name = snippetName;
                    snippet.Language = language;
                    snippet.Content = scintillaEditor.Text;
                    snippet.LastModified = DateTime.Now;
                    snippet.CodeType = codeType;
                }
                else
                {
                    // if ever wala but just to ensure
                    snippet = new Snippet
                    {
                        UserId = userId,
                        Name = snippetName,
                        Language = language,
                        Content = scintillaEditor.Text,
                        CodeType = codeType
                    };
                }
            }
            else
            {
                // Create a new snippet
                snippet = new Snippet
                {
                    UserId = userId,
                    Name = snippetName,
                    Language = language,
                    Content = scintillaEditor.Text,
                    CodeType = codeType
                };
            }

            SnippetManager.SaveSnippet(snippet);

            // Store the snippet ID in the editor's Tag property for future updates
            scintillaEditor.Tag = snippet.Id;
            this.Text = $"CodePlayground - Editing: {snippet.Name} ({snippet.CodeType} - {GetLanguageDisplayName(language)})";

            MessageBox.Show("Snippet saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GetLanguageDisplayName(string language)
        {
            switch (language.ToLower())
            {
                case "python":
                    return "Python";
                case "c":
                    return "C";
                case "cpp":
                    return "C++";
                default:
                    return language;
            }
        }

        private void ptbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // draggable form thingy napud note to self do not touch like before
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //Overridden methods
        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;  // Standard Title Bar - Snap Window
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020;
            const int SC_RESTORE = 0xF120;
            const int WM_NCHITTEST = 0x0084;   // Detects mouse position for resizing
            const int resizeAreaSize = 10;

            // Hit test values for resizing
            const int HTCLIENT = 1;
            const int HTLEFT = 10, HTRIGHT = 11, HTTOP = 12, HTTOPLEFT = 13, HTTOPRIGHT = 14;
            const int HTBOTTOM = 15, HTBOTTOMLEFT = 16, HTBOTTOMRIGHT = 17;

            if (m.Msg == WM_NCHITTEST && this.WindowState == FormWindowState.Normal)
            {
                base.WndProc(ref m);
                if ((int)m.Result == HTCLIENT)
                {
                    Point screenPoint = new Point(m.LParam.ToInt32());
                    Point clientPoint = this.PointToClient(screenPoint);

                    if (clientPoint.Y <= resizeAreaSize)
                    {
                        if (clientPoint.X <= resizeAreaSize) m.Result = (IntPtr)HTTOPLEFT;
                        else if (clientPoint.X >= this.Width - resizeAreaSize) m.Result = (IntPtr)HTTOPRIGHT;
                        else m.Result = (IntPtr)HTTOP;
                    }
                    else if (clientPoint.Y >= this.Height - resizeAreaSize)
                    {
                        if (clientPoint.X <= resizeAreaSize) m.Result = (IntPtr)HTBOTTOMLEFT;
                        else if (clientPoint.X >= this.Width - resizeAreaSize) m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTBOTTOM;
                    }
                    else if (clientPoint.X <= resizeAreaSize) m.Result = (IntPtr)HTLEFT;
                    else if (clientPoint.X >= this.Width - resizeAreaSize) m.Result = (IntPtr)HTRIGHT;
                }
                return;
            }

            // Remove window border but keep snapping functionality
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                m.Result = IntPtr.Zero;
                return;
            }

            // Keep form size consistent when minimizing/restoring
            if (m.Msg == WM_SYSCOMMAND)
            {
                int wParam = (m.WParam.ToInt32() & 0xFFF0);

                if (wParam == SC_MINIMIZE)
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)
                    this.Size = formSize;
            }

            base.WndProc(ref m);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(scintillaEditor.Text))
            {
                Clipboard.SetText(scintillaEditor.Text);
                scintillaEditor.SelectAll();
            }
        }

        public void LoadSnippet(Snippet snippet, bool updateTitle = true)
        {
            SetLanguage(snippet.Language, GetButtonForLanguage(snippet.Language));
            scintillaEditor.Text = snippet.Content;

            // Store the snippet ID for future updates
            scintillaEditor.Tag = snippet.Id;

            // Update window title to show we're editing an existing snippet
            if (updateTitle)
            {
                this.Text = $"CodePlayground - Editing: {snippet.Name} ({snippet.CodeType} - {GetLanguageDisplayName(snippet.Language)})";
            }
        }

        private Button GetButtonForLanguage(string language)
        {
            return language == "c" ? btnC : language == "cpp" ? btnCPP : btnPython;
        }

        private (string snippetName, string codeType) PromptForSnippetName(bool isEditing = false, string existingName = null, string existingCodeType = null)
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 350;
                prompt.Height = 250;
                prompt.Text = isEditing ? "Update Snippet" : "Save Snippet";
                prompt.StartPosition = FormStartPosition.CenterScreen;

                // Snippet Name
                Label nameLabel = new Label()
                {
                    Left = 20,
                    Top = 20,
                    Text = "Snippet Name:",
                    Width = 150
                };
                TextBox nameTextBox = new TextBox()
                {
                    Left = 20,
                    Top = 50,
                    Width = 300,
                    Text = existingName ?? "" // Pre-fill with existing name if editing
                };

                // Code Type Dropdown
                Label typeLabel = new Label()
                {
                    Left = 20,
                    Top = 90,
                    Text = "Code Type:",
                    Width = 150
                };
                ComboBox typeComboBox = new ComboBox()
                {
                    Left = 20,
                    Top = 120,
                    Width = 300,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                // Populate code type dropdown based on current language
                string[] codeTypes = GetCodeTypesForLanguage(GetSelectedLanguage());
                typeComboBox.Items.AddRange(codeTypes);

                // Set the selected code type if available
                if (!string.IsNullOrEmpty(existingCodeType) && codeTypes.Contains(existingCodeType))
                {
                    typeComboBox.SelectedItem = existingCodeType;
                }
                else
                {
                    typeComboBox.SelectedIndex = 0; // Default to first item
                }

                Button confirmation = new Button()
                {
                    Text = isEditing ? "Update" : "Save",
                    Left = 250,
                    Width = 70,
                    Top = 160
                };

                string selectedCodeType = typeComboBox.SelectedItem?.ToString() ?? "";

                typeComboBox.SelectedIndexChanged += (sender, e) =>
                {
                    selectedCodeType = typeComboBox.SelectedItem?.ToString() ?? "";
                };

                confirmation.Click += (sender, e) =>
                {
                    if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                    {
                        MessageBox.Show("Please enter a snippet name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    prompt.DialogResult = DialogResult.OK;
                    prompt.Close();
                };

                Button cancel = new Button()
                {
                    Text = "Cancel",
                    Left = 170,
                    Width = 70,
                    Top = 160
                };
                cancel.Click += (sender, e) =>
                {
                    prompt.DialogResult = DialogResult.Cancel;
                    prompt.Close();
                };

                prompt.Controls.Add(nameLabel);
                prompt.Controls.Add(nameTextBox);
                prompt.Controls.Add(typeLabel);
                prompt.Controls.Add(typeComboBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(cancel);

                prompt.AcceptButton = confirmation;
                prompt.CancelButton = cancel;

                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    return (nameTextBox.Text, selectedCodeType);
                }

                return (null, null);
            }
        }

        public string[] GetCodeTypesForLanguage(string language)
        {
            switch (language.ToLower())
            {
                case "python":
                    return new[] { "Script", "Function", "Class", "Module", "Utility" };
                case "c":
                    return new[] { "Program", "Function", "Header", "Utility", "Library" };
                case "cpp":
                    return new[] { "Program", "Function", "Class", "Header", "Utility" };
                default:
                    return new[] { "Unknown" };
            }
        }

        private string PrepareInputFile(string code)
        {
            // check if code likely requires user input (scanf, cin, getchar, etc.)
            bool requiresInput = code.Contains("scanf(") ||
                                code.Contains("cin >>") ||
                                code.Contains("getchar(") ||
                                code.Contains("gets(") ||
                                code.Contains("fgets(") ||
                                code.Contains("std::cin");

            if (!requiresInput)
                return null; // No input needed

            // find potential input prompts
            List<string> inputPrompts = new List<string>();

            // c printf to scanf
            if (code.Contains("printf(") && code.Contains("scanf("))
            {
                // This is a simple heuristic, not perfect but helpful
                int index = 0;
                while ((index = code.IndexOf("printf(", index)) >= 0)
                {
                    int closingIndex = FindClosingParenthesis(code, index + 7);
                    if (closingIndex > index)
                    {
                        string prompt = code.Substring(index + 7, closingIndex - (index + 7));
                        // Clean up the prompt a bit
                        prompt = prompt.Trim('\"', ' ', ',');
                        if (!string.IsNullOrWhiteSpace(prompt))
                        {
                            inputPrompts.Add(prompt);
                        }
                    }
                    index = closingIndex + 1;
                }
            }

            // c++ cout << before cin >>
            if (code.Contains("cout") && code.Contains("cin"))
            {
                int index = 0;
                while ((index = code.IndexOf("cout <<", index)) >= 0)
                {
                    int endIndex = code.IndexOf(";", index);
                    if (endIndex > index)
                    {
                        string line = code.Substring(index, endIndex - index);
                        // Look for string literals
                        int strStart = line.IndexOf("\"");
                        if (strStart >= 0)
                        {
                            int strEnd = line.IndexOf("\"", strStart + 1);
                            if (strEnd > strStart)
                            {
                                string prompt = line.Substring(strStart + 1, strEnd - strStart - 1);
                                if (!string.IsNullOrWhiteSpace(prompt))
                                {
                                    inputPrompts.Add(prompt);
                                }
                            }
                        }
                    }
                    index = endIndex + 1;
                }
            }

            // Show input dialog to the user
            string userInput = PromptForUserInput(inputPrompts);
            if (userInput == null)
                return null; // User cancelled input

            // Create an input file
            string inputFile = Path.Combine(Path.GetTempPath(), $"snipit_input_{Guid.NewGuid()}.txt");
            File.WriteAllText(inputFile, userInput);

            return inputFile;
        }

        private int FindClosingParenthesis(string text, int openPos)
        {
            int depth = 1;
            for (int i = openPos + 1; i < text.Length; i++)
            {
                if (text[i] == '(') depth++;
                else if (text[i] == ')')
                {
                    depth--;
                    if (depth == 0) return i;
                }
            }
            return -1; // No closing parenthesis found
        }

        private string PromptForUserInput(List<string> prompts)
        {
            using (Form inputDialog = new Form())
            {
                inputDialog.Text = "Program Input";
                inputDialog.StartPosition = FormStartPosition.CenterParent;
                inputDialog.MinimizeBox = false;
                inputDialog.MaximizeBox = false;
                inputDialog.Width = 1000;
                inputDialog.Height = 700;
                inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;

                Label instructionLabel = new Label
                {
                    Text = "The program you're running requires user input.\r\nEnter each input value on a new line:",
                    AutoSize = true,
                    Location = new Point(20, 20),
                    MaximumSize = new Size(940, 0)
                };

                Panel helpPanel = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(20, 80),
                    Size = new Size(940, 100),
                    AutoScroll = true
                };

                Label helpLabel = new Label
                {
                    Text = "Input Tips:\r\n" +
                           "• For numbers, just type the number (e.g., 42, 3.14)\r\n" +
                           "• For arrays, separate values with spaces (e.g., 1 2 3 4 5)\r\n" +
                           "• For strings with spaces, just type normally - the program will read as needed\r\n" +
                           "• For multiple inputs, put each on a separate line",
                    AutoSize = true,
                    Location = new Point(5, 5),
                    MaximumSize = new Size(920, 0)
                };

                helpPanel.Controls.Add(helpLabel);

                Panel promptsPanel = null;
                int nextTop = helpPanel.Bottom + 10;

                if (prompts.Count > 0)
                {
                    promptsPanel = new Panel
                    {
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(20, nextTop),
                        Size = new Size(940, 100),
                        AutoScroll = true
                    };

                    StringBuilder promptText = new StringBuilder("Detected input prompts:\r\n");
                    foreach (string prompt in prompts)
                    {
                        promptText.AppendLine($"• {prompt}");
                    }

                    Label promptsLabel = new Label
                    {
                        Text = promptText.ToString(),
                        AutoSize = true,
                        Location = new Point(5, 5),
                        MaximumSize = new Size(920, 0)
                    };

                    promptsPanel.Controls.Add(promptsLabel);
                    nextTop = promptsPanel.Bottom + 10;
                }

                TextBox inputTextBox = new TextBox
                {
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    Width = 940,
                    Height = 300,
                    Location = new Point(20, nextTop),
                    AcceptsReturn = true
                };

                Button okButton = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Location = new Point(780, inputTextBox.Bottom + 10),
                    Width = 80
                };

                Button cancelButton = new Button
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(680, inputTextBox.Bottom + 10),
                    Width = 80
                };

                inputDialog.Controls.Add(instructionLabel);
                inputDialog.Controls.Add(helpPanel);
                if (promptsPanel != null)
                    inputDialog.Controls.Add(promptsPanel);
                inputDialog.Controls.Add(inputTextBox);
                inputDialog.Controls.Add(okButton);
                inputDialog.Controls.Add(cancelButton);

                inputDialog.AcceptButton = okButton;
                inputDialog.CancelButton = cancelButton;

                return inputDialog.ShowDialog() == DialogResult.OK ? inputTextBox.Text : null;
            }
        }


        private string ExecuteCommandWithInput(string exe, string arguments, string inputFile)
        {
            // Create process
            var psi = new ProcessStartInfo
            {
                FileName = exe,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using var process = new Process { StartInfo = psi };

                var outputBuilder = new StringBuilder();
                var errorBuilder = new StringBuilder();

                process.OutputDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                        outputBuilder.AppendLine(e.Data);
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (e.Data != null)
                        errorBuilder.AppendLine(e.Data);
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                string[] inputLines = File.ReadAllLines(inputFile);
                foreach (string line in inputLines)
                {
                    process.StandardInput.WriteLine(line);
                }

                process.StandardInput.Close();

                if (!process.WaitForExit(30000))
                {
                    try
                    {
                        process.Kill();
                    }
                    catch
                    {
                    }
                    return "Process timed out.";
                }

                if (process.ExitCode != 0 && errorBuilder.Length > 0)
                {
                    return $"Error (Exit code: {process.ExitCode}):\n{errorBuilder}";
                }

                // Return combined output
                string result = outputBuilder.ToString().Trim();
                string error = errorBuilder.ToString().Trim();

                if (!string.IsNullOrEmpty(error))
                {
                    return $"{result}\n\nWarnings/Errors:\n{error}";
                }

                return result;
            }
            catch (Exception ex)
            {
                return $"Command execution failed: {ex.Message}";
            }
        }

        private void ptbMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                formSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }

            CodePlayground_Resize(this, EventArgs.Empty);
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (scintillaEditor == null || string.IsNullOrWhiteSpace(scintillaEditor.Text))
            {
                MessageBox.Show("No code to execute!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string language = GetSelectedLanguage();
                string code = scintillaEditor.Text;

                string codeType = GetOrPromptForCodeType(language);
                if (codeType == null)
                    return;

                if (language.ToLower() == "python")
                {
                    // pycharm export
                    ExportPythonCode(code, codeType);
                }
                else
                {
                    // c/c++ clion export
                    ExportToCLion(code, language, codeType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting code: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportPythonCode(string code, string codeType, string snippetName = null)
        {
            try
            {
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Select folder for Python project";
                    folderDialog.UseDescriptionForTitle = true;

                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string pythonProjectsPath = Path.Combine(documentsPath, "PythonProjects");

                    if (Directory.Exists(pythonProjectsPath))
                    {
                        folderDialog.SelectedPath = pythonProjectsPath;
                    }
                    else
                    {
                        folderDialog.SelectedPath = documentsPath;
                    }

                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        string projectName;
                        if (!string.IsNullOrEmpty(snippetName))
                        {
                            projectName = string.Join("_", snippetName.Split(Path.GetInvalidFileNameChars())).Replace(" ", "_");
                        }
                        else
                        {
                            projectName = $"SnipIt_{codeType}_{DateTime.Now:yyyyMMdd}";
                        }

                        // Check if directory already exists and create a unique name if needed
                        string projectPath = Path.Combine(folderDialog.SelectedPath, projectName);
                        int counter = 1;
                        string originalProjectName = projectName;
                        while (Directory.Exists(projectPath))
                        {
                            projectName = $"{originalProjectName}_{counter}";
                            projectPath = Path.Combine(folderDialog.SelectedPath, projectName);
                            counter++;
                        };

                        // to review in the future

                        // proj directory structure
                        Directory.CreateDirectory(projectPath);

                        // src directory
                        string srcDir = Path.Combine(projectPath, "src");
                        Directory.CreateDirectory(srcDir);

                        // tests directory
                        string testsDir = Path.Combine(projectPath, "tests");
                        Directory.CreateDirectory(testsDir);

                        // main module name
                        string mainModuleName = projectName.ToLower().Replace("-", "_");

                        // main module directory
                        string moduleDir = Path.Combine(srcDir, mainModuleName);
                        Directory.CreateDirectory(moduleDir);

                        // create __init__.py files to make directories packages
                        File.WriteAllText(Path.Combine(moduleDir, "__init__.py"), "# Package initialization");
                        File.WriteAllText(Path.Combine(testsDir, "__init__.py"), "# Test package initialization");

                        // Determine main file path based on code type
                        string mainFilePath;
                        if (codeType.ToLower() == "script")
                        {
                            mainFilePath = Path.Combine(projectPath, "main.py");
                        }
                        else if (codeType.ToLower() == "class")
                        {
                            mainFilePath = Path.Combine(moduleDir, GetFileNameFromSnippet(snippetName, "class"));
                        }
                        else if (codeType.ToLower() == "module")
                        {
                            mainFilePath = Path.Combine(moduleDir, "__init__.py");
                        }
                        else if (codeType.ToLower() == "function")
                        {
                            mainFilePath = Path.Combine(moduleDir, "utilities.py");
                        }
                        else
                        {
                            mainFilePath = Path.Combine(moduleDir, "main.py");
                        }

                        // Save the main code file
                        File.WriteAllText(mainFilePath, code);

                        // Create a simple test file
                        string testFileName = $"test_{Path.GetFileNameWithoutExtension(mainFilePath)}.py";
                        string testFilePath = Path.Combine(testsDir, testFileName);
                        string testTemplate = $@"# Test file for {Path.GetFileName(mainFilePath)}
                        import unittest
                        import sys
                        import os

                        # Add the src directory to the path so we can import our module
                        sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '..', 'src')))

                        from {mainModuleName} import *

                        class Test{CamelCase(Path.GetFileNameWithoutExtension(mainFilePath))}(unittest.TestCase):
                            def test_example(self):
                                # TODO: Add your tests here
                                self.assertTrue(True)

                        if __name__ == '__main__':
                            unittest.main()
                        ";
                        File.WriteAllText(testFilePath, testTemplate);

                        // requirements txt file if ever nay dependencies but ill leave it to the user, ill just keep it simple for now
                        File.WriteAllText(Path.Combine(projectPath, "requirements.txt"), "# List your dependencies here\n# Example:\n# numpy==1.21.0\n# pandas==1.3.0\n");

                        // Create a setup.py file for packaging
                        string setupPyContent = $@"from setuptools import setup, find_packages

                        setup(
                            name='{projectName}',
                            version='0.1.0',
                            packages=find_packages(where='src'),
                            package_dir={{'': 'src'}},
                            install_requires=[
                                # Add your dependencies here
                            ],
                            python_requires='>=3.6',
                            author='',
                            author_email='',
                            description='Project created with SnipIt',
                        )";
                        File.WriteAllText(Path.Combine(projectPath, "setup.py"), setupPyContent);

                        string readmeContent = $@"# {projectName}

                        Project created with SnipIt Code Playground on {DateTime.Now}

                        ## Description

                        A Python {codeType.ToLower()} exported from SnipIt.

                        ## Installation

                        ```
                        pip install -e .
                        ```

                        ## Usage

                        ```python
                        from {mainModuleName} import *
                        # Your code here
                        ```

                        ## Testing

                        ```
                        python -m unittest discover -s tests
                        ```
                        ";
                        File.WriteAllText(Path.Combine(projectPath, "README.md"), readmeContent);

                        // create PyCharm project files (.idea directory)
                        CreatePyCharmProjectFiles(projectPath, projectName);

                        // Ask if user wants to open the project in PyCharm
                        DialogResult openResult = MessageBox.Show(
                            $"Python project created successfully at:\n{projectPath}\n\nWould you like to open it with PyCharm?",
                            "Open with PyCharm",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (openResult == DialogResult.Yes)
                        {
                            // Get PyCharm path
                            string pycharmPath = GetIDEPath("python");

                            if (string.IsNullOrEmpty(pycharmPath))
                            {
                                pycharmPath = FindPyCharmPath();
                                if (!string.IsNullOrEmpty(pycharmPath))
                                {
                                    SaveIDEPath("python", pycharmPath);
                                }
                                else
                                {
                                    pycharmPath = SelectIDE("python");
                                    if (!string.IsNullOrEmpty(pycharmPath))
                                    {
                                        SaveIDEPath("python", pycharmPath);
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(pycharmPath))
                            {
                                // Open with PyCharm
                                Process.Start(new ProcessStartInfo
                                {
                                    FileName = pycharmPath,
                                    Arguments = $"\"{projectPath}\"",
                                    UseShellExecute = true
                                });
                            }
                            else
                            {
                                // Open with default program if PyCharm not selected but it shouldnt happen really
                                Process.Start(new ProcessStartInfo
                                {
                                    FileName = "explorer.exe",
                                    Arguments = $"\"{projectPath}\"",
                                    UseShellExecute = true
                                });
                            }
                        }
                        else
                        {
                            // Open folder containing the project
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = "explorer.exe",
                                Arguments = $"\"{projectPath}\"",
                                UseShellExecute = true
                            });
                        }

                        MessageBox.Show($"Python project created successfully at:\n{projectPath}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting Python code: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CamelCase(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            // split by non-alphanumeric characters and capitalize each part
            var parts = System.Text.RegularExpressions.Regex.Split(text, @"[^a-zA-Z0-9]")
                .Where(p => !string.IsNullOrEmpty(p))
                .Select(p => char.ToUpper(p[0]) + p.Substring(1));

            return string.Join("", parts);
        }

        private void CreatePyCharmProjectFiles(string projectPath, string projectName)
        {
            string ideaDir = Path.Combine(projectPath, ".idea");
            Directory.CreateDirectory(ideaDir);

            // Create main project file
            string projectImlContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <module type=""PYTHON_MODULE"" version=""4"">
                  <component name=""NewModuleRootManager"">
                    <content url=""file://$MODULE_DIR$"">
                      <sourceFolder url=""file://$MODULE_DIR$/src"" isTestSource=""false"" />
                      <sourceFolder url=""file://$MODULE_DIR$/tests"" isTestSource=""true"" />
                      <excludeFolder url=""file://$MODULE_DIR$/.venv"" />
                      <excludeFolder url=""file://$MODULE_DIR$/build"" />
                      <excludeFolder url=""file://$MODULE_DIR$/dist"" />
                    </content>
                    <orderEntry type=""inheritedJdk"" />
                    <orderEntry type=""sourceFolder"" forTests=""false"" />
                  </component>
                </module>";
            File.WriteAllText(Path.Combine(ideaDir, $"{projectName}.iml"), projectImlContent);

            // Create modules.xml
            string modulesXmlContent = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
                <project version=""4"">
                  <component name=""ProjectModuleManager"">
                    <modules>
                      <module fileurl=""file://$PROJECT_DIR$/.idea/{projectName}.iml"" filepath=""$PROJECT_DIR$/.idea/{projectName}.iml"" />
                    </modules>
                  </component>
                </project>";
            File.WriteAllText(Path.Combine(ideaDir, "modules.xml"), modulesXmlContent);

            // Create misc.xml
            string miscXmlContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <project version=""4"">
                  <component name=""ProjectRootManager"" version=""2"" project-jdk-name=""Python 3"" project-jdk-type=""Python SDK"" />
                </project>";
            File.WriteAllText(Path.Combine(ideaDir, "misc.xml"), miscXmlContent);
        }
        private string GetFileNameFromSnippet(string snippetName, string codeType)
        {
            if (string.IsNullOrEmpty(snippetName))
            {
                // default naming convention based on code type
                switch (codeType.ToLower())
                {
                    case "class": return "my_class.py";
                    case "function": return "utilities.py";
                    case "module": return "module.py";
                    default: return "main.py";
                }
            }

            // convert to snake_case
            string snakeCase = snippetName.ToLower()
                .Replace(" ", "_")
                .Replace("-", "_");

            // remove any invalid characters
            snakeCase = new string(snakeCase.Where(c => char.IsLetterOrDigit(c) || c == '_').ToArray());

            // ensure it starts with a letter or underscore (Python requirement)
            if (snakeCase.Length > 0 && !char.IsLetter(snakeCase[0]) && snakeCase[0] != '_')
            {
                snakeCase = "_" + snakeCase;
            }

            return snakeCase + ".py";
        }

        // code type from editor tag or prompt user
        private string GetOrPromptForCodeType(string language)
        {
            string codeType = null;

            // code type from editor's tag
            if (scintillaEditor.Tag != null)
            {
                if (scintillaEditor.Tag is Dictionary<string, object> metadata &&
                    metadata.TryGetValue("CodeType", out object type) &&
                    type != null)
                {
                    codeType = type.ToString();
                }
                else if (scintillaEditor.Tag is Snippet snippet && !string.IsNullOrEmpty(snippet.CodeType))
                {
                    codeType = snippet.CodeType;
                }
            }

            // If code type not found, prompt user
            if (string.IsNullOrEmpty(codeType))
            {
                codeType = PromptForCodeType(language);
            }

            return codeType;
        }

        private string PromptForCodeType(string language) // make prompt asking for what code type
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 350;
                prompt.Height = 200;
                prompt.Text = "Select Code Type";
                prompt.StartPosition = FormStartPosition.CenterScreen;
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.MaximizeBox = false;
                prompt.MinimizeBox = false;

                Label typeLabel = new Label()
                {
                    Left = 20,
                    Top = 20,
                    Text = "Code Type:",
                    Width = 150
                };

                ComboBox typeComboBox = new ComboBox()
                {
                    Left = 20,
                    Top = 50,
                    Width = 300,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                // Populate code type dropdown based on language they picked
                string[] codeTypes = GetCodeTypesForLanguage(language);
                typeComboBox.Items.AddRange(codeTypes);
                typeComboBox.SelectedIndex = 0;

                Button confirmation = new Button()
                {
                    Text = "OK",
                    Left = 250,
                    Width = 70,
                    Top = 100,
                    DialogResult = DialogResult.OK
                };

                Button cancel = new Button()
                {
                    Text = "Cancel",
                    Left = 170,
                    Width = 70,
                    Top = 100,
                    DialogResult = DialogResult.Cancel
                };

                prompt.Controls.Add(typeLabel);
                prompt.Controls.Add(typeComboBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(cancel);

                prompt.AcceptButton = confirmation;
                prompt.CancelButton = cancel;

                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    return typeComboBox.SelectedItem.ToString();
                }

                return null;
            }
        }

        // Get appropriate file extension based on language and code type
        private string GetFileExtensionForLanguageAndType(string language, string codeType)
        {
            switch (language.ToLower())
            {
                case "python":
                    return ".py";

                case "c":
                    switch (codeType.ToLower())
                    {
                        case "header":
                            return ".h";
                        case "library":
                            return ".lib";
                        default:
                            return ".c";
                    }

                case "cpp":
                    switch (codeType.ToLower())
                    {
                        case "header":
                            return ".h";
                        case "class":
                            return ".hpp";
                        case "library":
                            return ".lib";
                        default:
                            return ".cpp";
                    }

                default:
                    return ".txt";
            }
        }

    
        private void ExportToIDE(string filePath, string language) // snipit to ide export for more advanced code
        {
            try
            {
                // Get specific IDE path based on language
                string idePath = GetIDEPath(language);

                if (string.IsNullOrEmpty(idePath))
                {
                    // If IDE path is not configured, ask user what to do
                    var result = MessageBox.Show(
                        $"No IDE configured for {language}. Would you like to select an IDE?",
                        "IDE Selection",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        idePath = SelectIDE(language);
                        if (!string.IsNullOrEmpty(idePath))
                        {
                            SaveIDEPath(language, idePath);
                        }
                    }
                    else
                    {
                        // open with default application tho it shouldnt really happen; need more tests
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = filePath,
                            UseShellExecute = true
                        });
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(idePath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = idePath,
                        Arguments = $"\"{filePath}\"",
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open IDE: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ide path 
        private string GetIDEPath(string language)
        {
            try
            {
                string configFile = GetIDEConfigFilePath();
                if (!File.Exists(configFile))
                {
                    return null;
                }

                string json = File.ReadAllText(configFile);
                var ideConfig = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                if (ideConfig != null && ideConfig.TryGetValue(language.ToLower(), out string path))
                {
                    return path;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private void SaveIDEPath(string language, string path)
        {
            // may or may not include in final output but will keep it for future reference
            try
            {
                string configFile = GetIDEConfigFilePath();
                Dictionary<string, string> ideConfig;

                if (File.Exists(configFile))
                {
                    string json = File.ReadAllText(configFile);
                    ideConfig = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                }
                else
                {
                    ideConfig = new Dictionary<string, string>();
                }

                ideConfig[language.ToLower()] = path;
                string newJson = JsonSerializer.Serialize(ideConfig, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(configFile, newJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save IDE configuration: {ex.Message}", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetIDEConfigFilePath()
        {
            string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SnipIt");

            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
            }

            return Path.Combine(appDataFolder, "ide_config.json");
        }

        private string SelectIDE(string language)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string ideType = language.ToLower() == "python" ? "PyCharm" : "CLion";
                openFileDialog.Title = $"Select {ideType} Installation";
                openFileDialog.Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

                string jetbrainsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "JetBrains");
                if (Directory.Exists(jetbrainsPath))
                {
                    openFileDialog.InitialDirectory = jetbrainsPath;
                }

                string idePath = language.ToLower() == "python" ? FindPyCharmPath() : FindCLionPath();
                if (!string.IsNullOrEmpty(idePath))
                {
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(idePath);
                    openFileDialog.FileName = Path.GetFileName(idePath);
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }

                return null;
            }
        }

        private string FindPyCharmPath()
        {
            try
            {
                // pycharm installation paths; all possible ones just to make sure
                string[] possiblePaths = new[]
                {
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "JetBrains", "PyCharm", "bin", "pycharm64.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "JetBrains", "PyCharm Community Edition", "bin", "pycharm64.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "JetBrains", "PyCharm", "bin", "pycharm.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "JetBrains", "PyCharm Community Edition", "bin", "pycharm.exe")
        };

                foreach (string path in possiblePaths)
                {
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }


        private string FindCLionPath()
        {
            try
            {
                // clion installation paths; all possible ones just to make sure cos im traumatized by dev c
                string[] possiblePaths = new[]
                {
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "JetBrains", "CLion", "bin", "clion64.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "JetBrains", "CLion 2023.3", "bin", "clion64.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "JetBrains", "CLion 2023.2", "bin", "clion64.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "JetBrains", "CLion 2023.1", "bin", "clion64.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "JetBrains", "CLion 2022.3", "bin", "clion64.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "JetBrains", "CLion", "bin", "clion.exe"),
        };

                foreach (string path in possiblePaths)
                {
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private string GenerateCMakeLists(string projectName, string language, string codeType, string sourceFileName)
        {
            StringBuilder cmakeContent = new StringBuilder();

            cmakeContent.AppendLine("cmake_minimum_required(VERSION 3.15)");

            switch (language.ToLower())
            {
                case "c":
                    cmakeContent.AppendLine($"project({projectName} C)");
                    cmakeContent.AppendLine("\nset(CMAKE_C_STANDARD 11)");
                    break;

                case "cpp":
                    cmakeContent.AppendLine($"project({projectName} CXX)");
                    cmakeContent.AppendLine("\nset(CMAKE_CXX_STANDARD 17)");
                    break;
            }

            // add executable or library based on code type
            switch (codeType.ToLower())
            {
                case "library":
                    cmakeContent.AppendLine($"\nadd_library({projectName} {sourceFileName})");
                    break;

                case "header":
                    // dummy exe for headers
                    
                    string dummyFile = "dummy.c";
                    if (language.ToLower() == "cpp")
                    {
                        dummyFile = "dummy.cpp";
                    }

                    // create the dummy file that includes the header
                    string dummyContent = $"// Auto-generated dummy file to include the header\n#include \"{sourceFileName}\"\n\nint main() {{\n    return 0;\n}}";
                    File.WriteAllText(Path.Combine(Path.GetDirectoryName(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SnipIt")), dummyFile), dummyContent);

                    cmakeContent.AppendLine($"\nadd_executable({projectName} {dummyFile})");
                    break;

                default:
                    cmakeContent.AppendLine($"\nadd_executable({projectName} {sourceFileName})");
                    break;
            }

            return cmakeContent.ToString();
        }

        private string GetSourceFileName(string language, string codeType, string snippetName = null)
        {
            // If a snippet name is provided, use it as the base filename
            if (!string.IsNullOrEmpty(snippetName))
            {
                // Remove invalid filename characters
                string validFileName = string.Join("_", snippetName.Split(Path.GetInvalidFileNameChars()));

                // Append appropriate extension based on language and code type
                switch (language.ToLower())
                {
                    case "c":
                        switch (codeType.ToLower())
                        {
                            case "header":
                                return $"{validFileName}.h";
                            case "library":
                                return $"{validFileName}.c";
                            default:
                                return $"{validFileName}.c";
                        }

                    case "cpp":
                        switch (codeType.ToLower())
                        {
                            case "header":
                                return $"{validFileName}.h";
                            case "class":
                                return $"{validFileName}.hpp";
                            case "library":
                                return $"{validFileName}.cpp";
                            default:
                                return $"{validFileName}.cpp";
                        }

                    case "python":
                        return $"{validFileName}.py";

                    default:
                        return $"{validFileName}.txt";
                }
            }

            // If no snippet name is available, fall back to the default naming nalang
            switch (language.ToLower())
            {
                case "c":
                    switch (codeType.ToLower())
                    {
                        case "header":
                            return "header.h";
                        case "library":
                            return "lib.c";
                        default:
                            return "main.c";
                    }

                case "cpp":
                    switch (codeType.ToLower())
                    {
                        case "header":
                            return "header.h";
                        case "class":
                            return "class.cpp";
                        case "library":
                            return "lib.cpp";
                        default:
                            return "main.cpp";
                    }

                case "python":
                    return "main.py";

                default:
                    return "main.txt";
            }
        }

        private void ExportToCLion(string code, string language, string codeType)
        {
            try
            {
                // ask user where they want to save the CLion project
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Select folder for CLion project";
                    folderDialog.UseDescriptionForTitle = true;

                    // try to set initial directory to a reasonable location
                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string projectsPath = Path.Combine(documentsPath, "CLionProjects");

                    if (Directory.Exists(projectsPath))
                    {
                        folderDialog.SelectedPath = projectsPath;
                    }
                    else
                    {
                        folderDialog.SelectedPath = documentsPath;
                    }

                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Create project folder structure
                        string projectName = $"SnipIt_{codeType}_{DateTime.Now:yyyyMMdd}";
                        string projectPath = Path.Combine(folderDialog.SelectedPath, projectName);

                        // Check if directory already exists and create a unique name if needed
                        int counter = 1;
                        string originalProjectName = projectName;
                        while (Directory.Exists(projectPath))
                        {
                            projectName = $"{originalProjectName}_{counter}";
                            projectPath = Path.Combine(folderDialog.SelectedPath, projectName);
                            counter++;
                        }

                        // project directory
                        Directory.CreateDirectory(projectPath);

                        // Determine source file name based on language and code type
                        string sourceFileName = GetSourceFileName(language, codeType);
                        string sourceFilePath = Path.Combine(projectPath, sourceFileName);

                        File.WriteAllText(sourceFilePath, code);
                        string cmakeFile = GenerateCMakeLists(projectName, language, codeType, sourceFileName);
                        File.WriteAllText(Path.Combine(projectPath, "CMakeLists.txt"), cmakeFile);

                        string readmeContent = $"# {projectName}\n\nCreated with SnipIt Code Playground on {DateTime.Now}\n\n## Description\n\nA {GetLanguageDisplayName(language)} {codeType.ToLower()} exported from SnipIt.";
                        File.WriteAllText(Path.Combine(projectPath, "README.md"), readmeContent);

                        // Ask if user wants to open the project in CLion
                        DialogResult openResult = MessageBox.Show(
                            $"CLion project created successfully at:\n{projectPath}\n\nWould you like to open it with CLion?",
                            "Open with CLion",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (openResult == DialogResult.Yes)
                        {
                            // Get CLion path
                            string clionPath = GetIDEPath("clion");

                            if (string.IsNullOrEmpty(clionPath))
                            {
                                clionPath = FindCLionPath();
                                if (!string.IsNullOrEmpty(clionPath))
                                {
                                    SaveIDEPath("clion", clionPath);
                                }
                                else
                                {
                                    clionPath = SelectIDE("clion");
                                    if (!string.IsNullOrEmpty(clionPath))
                                    {
                                        SaveIDEPath("clion", clionPath);
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(clionPath))
                            {
                                // Open with CLion
                                Process.Start(new ProcessStartInfo
                                {
                                    FileName = clionPath,
                                    Arguments = $"\"{projectPath}\"",
                                    UseShellExecute = true
                                });
                            }
                            else
                            {
                                // Just open the folder if CLion not found
                                Process.Start(new ProcessStartInfo
                                {
                                    FileName = "explorer.exe",
                                    Arguments = $"\"{projectPath}\"",
                                    UseShellExecute = true
                                });
                            }
                        }
                        else
                        {
                            // Open folder containing the project
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = "explorer.exe",
                                Arguments = $"\"{projectPath}\"",
                                UseShellExecute = true
                            });
                        }

                        MessageBox.Show($"CLion project created successfully at:\n{projectPath}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting code to CLion: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}