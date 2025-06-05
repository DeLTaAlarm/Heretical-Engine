/*using Microsoft.Xna.Framework;
using ProjectSpaceProject.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Data;
using ProjectSpaceProject.EventManage;


namespace ProjectSpaceProject
{
    public class ServerSystemManager
    {
        public GameScene gameWorld;
        public List<Entity> entitiesToDestroy = new List<Entity>();
        public List<BaseComponent> componentsToDestroy = new List<BaseComponent>();
        public HashSet<(Entity, Entity)> collisionOriginTargetPairs = new HashSet<(Entity, Entity)>();
        public Rectangle mapBounds;

        public void Update(GameTime gameTime)
        {
            foreach (BaseProcessingSystem sys in gameWorld.systems)
            {
                sys.Update(gameTime);
            }
            DestroyEntities();
            //DestroyComponents(componentsToDestroy);
        }

        private void ServerInitialize()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<Type> types = new List<Type>();
            List<Type> tempComps = assembly.GetTypes().Where(t => t.IsClass && t != typeof(BaseComponent) && typeof(BaseComponent).IsAssignableFrom(t)).ToList();
            List<BaseProcessingSystem> tempSys = new List<BaseProcessingSystem>();
            List<BaseRenderingSystem> tempRendSys = new List<BaseRenderingSystem>();

            foreach (Type t in assembly.GetTypes().Where(t => t.IsClass && t != typeof(BaseProcessingSystem) && typeof(BaseProcessingSystem).IsAssignableFrom(t)).ToList()) 
            {
                tempSys.Add((BaseProcessingSystem)t.GetConstructors()[0].Invoke(new object[0]{ }));         //Берёт тип системы, и вызывает его конструктор.
            }
            List<BaseProcessingSystem> tList = (from t in tempSys 
                                     where t.GetType().GetCustomAttribute(typeof(SystemsOrderAttribute)) is not null 
                                     orderby (t.GetType().GetCustomAttribute(typeof(SystemsOrderAttribute)) as SystemsOrderAttribute).orderType, 
                                             (t.GetType().GetCustomAttribute(typeof(SystemsOrderAttribute)) as SystemsOrderAttribute).order
                                      select t)
                                     .Concat(from t in tempSys
                                                       where t.GetType().GetCustomAttribute(typeof(SystemsOrderAttribute)) is null
                                                       select t).ToList();

            foreach (Type t in assembly.GetTypes().Where(t => t.IsClass && t != typeof(BaseRenderingSystem) && typeof(BaseRenderingSystem).IsAssignableFrom(t)).ToList())
            {
                tempRendSys.Add((BaseRenderingSystem)t.GetConstructors()[0].Invoke(new object[0] { }));
            }

            //Манипуляции при помощи LINQ нужны для создания порядка работы систем на основании данных атрибута SystemsOrderAttribute
            gameWorld.systems = tList.ToArray();
            for (int i = 0; i < tempComps.Count; i++)
            {
                gameWorld.componentsByType.Add(tempComps[i], (new List<BaseComponent>(), i));
            }

            gameWorld.sceneEventManager = new EventsManager();

            
            Dictionary<SystemsRenderingOrderEnum, List<BaseRenderingSystem>> tRenderSystems = new Dictionary<SystemsRenderingOrderEnum, List<BaseRenderingSystem>>();
            foreach (SystemsRenderingOrderEnum s in Enum.GetValues(typeof(SystemsRenderingOrderEnum)))
            {
                tRenderSystems[s] = new List<BaseRenderingSystem>();
            }

            foreach (BaseRenderingSystem s in tempRendSys)
            {
                SystemsRenderingOrderAttribute tempAttr = s.GetType().GetCustomAttribute<SystemsRenderingOrderAttribute>();
                if (tempAttr != null)tRenderSystems[tempAttr.orderType].Add(s as BaseRenderingSystem);
            }

            gameWorld.renderingSystems = new Dictionary<SystemsRenderingOrderEnum, BaseRenderingSystem[]>();

            foreach (SystemsRenderingOrderEnum s in Enum.GetValues(typeof(SystemsRenderingOrderEnum)))
            {
                gameWorld.renderingSystems[s] = tRenderSystems[s]
                    .OrderBy(sys => sys.GetType().GetCustomAttribute<SystemsRenderingOrderAttribute>()?.order ?? 0)
                    .ToArray();
            }

            foreach (BaseProcessingSystem s in tempSys) //Инициализирует системы
            {
                s.gameScene = gameWorld;
                s.Initialize();
            }

            foreach(BaseRenderingSystem s in tempRendSys)
            {
                s.gameScene = gameWorld;
                s.Initialize();
            }
        }

        public ServerSystemManager(GameScene _gameWorld)
        {
            gameWorld = _gameWorld;
            ServerInitialize();
        }
    }
}
*/