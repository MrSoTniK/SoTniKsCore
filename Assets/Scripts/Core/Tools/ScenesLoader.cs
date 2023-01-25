using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Tools 
{ 
    public class ScenesLoader
    {
        public event Action SceneIsLoaded;
        public event Action SceneIsUnLoaded;

        private static readonly Lazy<ScenesLoader> lazy =
         new Lazy<ScenesLoader>(() => new ScenesLoader());

        private static List<string> _loadedScenesList = new List<string>();

        public static ScenesLoader Instance { get { return lazy.Value; } }
        public string CurrentScene { get; private set; }

        public static void LoadScenePath(string scenePath) 
        {
            AsyncOperation handler = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
            handler.allowSceneActivation = true;

            handler.completed += load =>
            {
                _loadedScenesList.Add(scenePath);
                Instance.CurrentScene = scenePath;
                SceneManager.SetActiveScene(SceneManager.GetSceneByPath("Assets/" + scenePath + ".unity"));
                Instance.SceneIsLoaded?.Invoke();
            };
        }

        public static void UnloadScenePath(string scenePath)
        {
            if (_loadedScenesList.Contains(scenePath)) 
            {
                AsyncOperation handler = SceneManager.UnloadSceneAsync(scenePath);

                handler.completed += unload =>
                {
                    _loadedScenesList.Remove(scenePath);
                    Instance.SceneIsUnLoaded?.Invoke();
                };
            }
        }

        public void Clear() 
        {
            _loadedScenesList.Clear();
            _loadedScenesList = new();
        }
    }
}