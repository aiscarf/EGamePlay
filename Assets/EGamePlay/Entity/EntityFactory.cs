﻿using System;
using System.Collections.Generic;

namespace EGamePlay
{
    public static class EntityFactory
    {
        public static GlobalEntity GlobalEntity { get; set; }
        public static bool DebugLog { get; set; } = false;

        private static T New<T>() where T : Entity, new()
        {
            var entity = new T();
            entity.InstanceId = IdFactory.NewInstanceId();
            if (!GlobalEntity.Entities.ContainsKey(typeof(T)))
            {
                GlobalEntity.Entities.Add(typeof(T), new List<Entity>());
            }
            GlobalEntity.Entities[typeof(T)].Add(entity);
            return entity;
        }

        public static T Create<T>() where T : Entity, new()
        {
            var entity = New<T>();
            entity.Id = entity.InstanceId;
            GlobalEntity.AddChild(entity);
            entity.Awake();
            if (DebugLog) Log.Debug($"EntityFactory->Create, {typeof(T).Name}={entity.InstanceId}");
            return entity;
        }

        public static T Create<T>(object initData) where T : Entity, new()
        {
            var entity = New<T>();
            entity.Id = entity.InstanceId;
            GlobalEntity.AddChild(entity);
            entity.Awake(initData);
            if (DebugLog) Log.Debug($"EntityFactory->Create, {typeof(T).Name}={entity.InstanceId}, {initData}");
            return entity;
        }

        public static T CreateWithParent<T>(Entity parent) where T : Entity, new()
        {
            var entity = New<T>();
            entity.Id = entity.InstanceId;
            parent.AddChild(entity);
            entity.Awake();
            if (DebugLog) Log.Debug($"EntityFactory->CreateWithParent, {parent.GetType().Name}, {typeof(T).Name}={entity.InstanceId}");
            return entity;
        }

        public static T CreateWithParent<T>(Entity parent, object initData) where T : Entity, new()
        {
            var entity = New<T>();
            entity.Id = entity.InstanceId;
            parent.AddChild(entity);
            entity.Awake(initData);
            if (DebugLog) Log.Debug($"EntityFactory->CreateWithParent, {parent.GetType().Name}, {typeof(T).Name}={entity.InstanceId}");
            return entity;
        }
    }
}