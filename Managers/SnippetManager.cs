using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SnipIt.Models;

namespace SnipIt.Managers
{
    public static class SnippetManager
    {
        private static readonly string DataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        private static readonly string UsersDirectory = Path.Combine(DataDirectory, "Users");

        static SnippetManager()
        {
            // Ensure the main data directory exists
            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }

            // Ensure the users directory exists
            if (!Directory.Exists(UsersDirectory))
            {
                Directory.CreateDirectory(UsersDirectory);
            }
        }

        private static string GetUserSnippetsPath(string userId)
        {
            // Create a user-specific directory if it doesn't exist
            string userDirectory = Path.Combine(UsersDirectory, userId);
            if (!Directory.Exists(userDirectory))
            {
                Directory.CreateDirectory(userDirectory);
            }

            return Path.Combine(userDirectory, "snippets.json");
        }

        public static void SaveSnippet(Snippet snippet)
        {
            // Validate User ID
            if (string.IsNullOrEmpty(snippet.UserId))
            {
                throw new ArgumentException("Snippet must have a valid UserId");
            }


            // Get user-specific file path
            string userSnippetsFile = GetUserSnippetsPath(snippet.UserId);
            List<Snippet> snippets = LoadUserSnippets(snippet.UserId);

            // check if snippet exists
            int existingIndex = snippets.FindIndex(s => s.Id == snippet.Id);
            if (existingIndex >= 0)
            {
                snippets[existingIndex] = snippet;
            }
            else
            {
                snippets.Add(snippet);
            }

            // save user snippets back to the user's file
            string json = JsonSerializer.Serialize(snippets, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(userSnippetsFile, json);
        }

        public static List<Snippet> LoadAllSnippets()
        {
            List<Snippet> allSnippets = new List<Snippet>();

            // Check if users directory exists
            if (!Directory.Exists(UsersDirectory))
            {
                return allSnippets;
            }

            // Get all user directories
            string[] userDirectories = Directory.GetDirectories(UsersDirectory);

            // Load snippets from each user's file
            foreach (string userDir in userDirectories)
            {
                string userId = Path.GetFileName(userDir);
                allSnippets.AddRange(LoadUserSnippets(userId));
            }

            return allSnippets;
        }

        public static List<Snippet> LoadUserSnippets(string userId)
        {
            // actual userid from session manager from login form
            string actualUserId = userId;
            if (string.IsNullOrEmpty(actualUserId))
            {
                // Fallback to session user ID if not provided
                actualUserId = timer.SessionManager.UserId.ToString();
                if (string.IsNullOrEmpty(actualUserId))
                {
                    return new List<Snippet>();
                }
            }

            string userSnippetsFile = GetUserSnippetsPath(actualUserId);

            // If the user's snippets file doesn't exist yet, return an empty list
            if (!File.Exists(userSnippetsFile))
            {
                // Create an empty snippets file for this user
                File.WriteAllText(userSnippetsFile, "[]");
                return new List<Snippet>();
            }

            // Read the user's snippets file
            string json = File.ReadAllText(userSnippetsFile);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Snippet>();
            }

            try
            {
                return JsonSerializer.Deserialize<List<Snippet>>(json) ?? new List<Snippet>();
            }
            catch
            {
                return new List<Snippet>();
            }
        }

        public static void DeleteSnippet(string snippetId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = timer.SessionManager.UserId.ToString();
                if (string.IsNullOrEmpty(userId))
                {
                    return;
                }
            }

            List<Snippet> snippets = LoadUserSnippets(userId);
            snippets.RemoveAll(s => s.Id == snippetId);

            string userSnippetsFile = GetUserSnippetsPath(userId);
            string json = JsonSerializer.Serialize(snippets, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(userSnippetsFile, json);
        }

        public static void DeleteSnippet(string snippetId)
        {
            string userId = timer.SessionManager.UserId.ToString();
            if (!string.IsNullOrEmpty(userId))
            {
                DeleteSnippet(snippetId, userId);
            }
        }

        // Overload for integer userId just to ensure stuff
        public static void DeleteAllUserSnippets(int userId)
        {
            if (userId <= 0)
            {
                return;
            }

            DeleteAllUserSnippets(userId.ToString());
        }

        public static void DeleteAllUserSnippets(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = timer.SessionManager.UserId.ToString();
                if (string.IsNullOrEmpty(userId))
                {
                    return;
                }
            }

            string userSnippetsFile = GetUserSnippetsPath(userId);

            if (File.Exists(userSnippetsFile))
            {
                File.WriteAllText(userSnippetsFile, "[]");
            }
        }

        // unused old method tho i switched to hybrid implementation using json
        // will keep for future reference
        public static void MigrateToUserSpecificFiles()
        {
            string oldSnippetsFile = Path.Combine(DataDirectory, "snippets.json");

            // Check if the old file exists
            if (File.Exists(oldSnippetsFile))
            {
                try
                {
                    string json = File.ReadAllText(oldSnippetsFile);
                    List<Snippet> allSnippets = JsonSerializer.Deserialize<List<Snippet>>(json) ?? new List<Snippet>();

                    // Group snippets by user ID
                    Dictionary<string, List<Snippet>> userSnippets = new Dictionary<string, List<Snippet>>();

                    foreach (Snippet snippet in allSnippets)
                    {
                        if (!string.IsNullOrEmpty(snippet.UserId))
                        {
                            if (!userSnippets.ContainsKey(snippet.UserId))
                            {
                                userSnippets[snippet.UserId] = new List<Snippet>();
                            }
                            userSnippets[snippet.UserId].Add(snippet);
                        }
                    }

                    // Save each user's snippets to their dedicated file
                    foreach (var kvp in userSnippets)
                    {
                        string userId = kvp.Key;
                        List<Snippet> snippets = kvp.Value;

                        string userSnippetsFile = GetUserSnippetsPath(userId);
                        string userJson = JsonSerializer.Serialize(snippets, new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText(userSnippetsFile, userJson);
                    }

                    // Optionally rename or backup the old file
                    File.Move(oldSnippetsFile, Path.Combine(DataDirectory, "snippets.json.bak"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error migrating snippets: {ex.Message}");
                }
            }
        }
    }
}