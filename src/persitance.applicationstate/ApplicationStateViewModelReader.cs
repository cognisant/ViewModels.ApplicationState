﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CR.ViewModels.Core;

namespace CR.ViewModels.Persitance.ApplicationState
{
    public class ApplicationStateViewModelReader : IViewModelReader
    {
        private HttpApplicationStateBase AppState { get; set; }

        public ApplicationStateViewModelReader(HttpApplicationStateBase appState)
        {
            AppState = appState;
        }

        public TEntity GetByKey<TEntity>(string key) where TEntity : class
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (key == "")
                throw new ArgumentException("key must not be an empty string", "key");

            var entities = GetEntities<TEntity>();

            if (entities == null)
                return null;

            TEntity result;
            return entities.TryGetValue(key, out result) ? result : null;
        }

        public IEnumerable<TEntity> Query<TEntity>(Func<TEntity, bool> predicate) where TEntity : class
        {
            var entities = GetEntities<TEntity>();
            return entities == null ? new List<TEntity>() : entities.Values.Where(predicate);
        }

        private Dictionary<String, TEntity> GetEntities<TEntity>()
        {
            var appStateKey = typeof(TEntity).FullName;
            return (Dictionary<string, TEntity>)AppState[appStateKey];
        }
    }
}