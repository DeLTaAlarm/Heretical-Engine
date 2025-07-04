using HDataCore;
using HSystemsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HCore
{
    /// <summary>
    /// Класс, обеспечивающий загрузку систем и компонентов из сборок.
    /// </summary>
    public class AssemblyLoader
    {
        private List<Assembly> systemsAssemblies = new List<Assembly>();
        private List<(Assembly, Type)> loadedProcessingSystems = new List<(Assembly, Type)>();
        private List<(Assembly, Type)> loadedRenderingSystems = new List<(Assembly, Type)>();

        /// <summary>
        /// Добавляет указанную сборку в загрузчик систем.
        /// </summary>
        /// <param name="assembly"> Добавляемая сборка</param>
        public void LoadAssembly(Assembly assembly)
        {
            systemsAssemblies.Add(assembly);
        }

        /// <summary>
        /// Добавляет исполняемую сборку в загрузчик систем.
        /// </summary>
        public void LoadAssembly()
        {
            systemsAssemblies.Add(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Добавляет сборку в загрузчик систем по указанному пути.
        /// </summary>
        /// <param name="path"> Путь до сборки</param>
        public void LoadAssembly(string path)
        {
            systemsAssemblies.Add(Assembly.Load(path));
        }

        /// <summary>
        /// Очищает загрузчик систем от всех сборок
        /// </summary>
        public void ClearAssemblies()
        {
            systemsAssemblies.Clear();
        }

        /// <summary>
        /// Находит и загружает все системы из добавленных сборок.
        /// </summary>
        public void LoadSystemsFromAssemblies()
        {
            foreach (Assembly assembly in systemsAssemblies)
            {
                foreach (Type currentType in assembly.GetTypes())
                {
                    if (currentType.IsClass)
                    {
                        if (typeof(BaseProcessingSystem).IsAssignableFrom(currentType) && currentType != typeof(BaseProcessingSystem))
                        {
                            loadedProcessingSystems.Add((assembly, currentType));
                        }
                        else if (typeof(BaseRenderingSystem).IsAssignableFrom(currentType) && currentType != typeof(BaseRenderingSystem))
                        {
                            loadedRenderingSystems.Add((assembly, currentType));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Создаёт найденные системы обработки и возвращает их в виде отсортированного массива.
        /// </summary>
        /// <returns>
        /// Возвращает отсортированный массив систем обработки
        /// </returns>
        public BaseProcessingSystem[] CreateProcessingSystems()
        {
            List<Type> types = new List<Type>();
            //List<Type> tempComps = assembly.GetTypes().Where(t => t.IsClass && t != typeof(BaseComponent) && typeof(BaseComponent).IsAssignableFrom(t)).ToList();
            List<BaseProcessingSystem> outputProcessingSystems = new List<BaseProcessingSystem>(); 

            /*foreach (Type t in assembly.GetTypes().Where(t => t.IsClass && t != typeof(BaseProcessingSystem) && typeof(BaseProcessingSystem).IsAssignableFrom(t)).ToList())
            {
                tempSys.Add((BaseProcessingSystem)t.GetConstructors()[0].Invoke(new object[0] { }));         //Берёт тип системы, и вызывает его конструктор.
            }*/

            for (int i = 0; i < loadedProcessingSystems.Count; i++)
            {
                outputProcessingSystems.Add((BaseProcessingSystem)loadedProcessingSystems[i].Item2.GetConstructors()[0].Invoke(new object[0] { }));
            }
            
            List<BaseProcessingSystem> outputSortedProcessingSystems = (from t in outputProcessingSystems
                                                where t.GetType().GetCustomAttribute(typeof(SystemsOrderAttribute)) is not null
                                                orderby (t.GetType().GetCustomAttribute(typeof(SystemsOrderAttribute)) as SystemsOrderAttribute).orderType,
                                                        (t.GetType().GetCustomAttribute(typeof(SystemsOrderAttribute)) as SystemsOrderAttribute).order
                                                select t)
                                     .Concat(from t in outputProcessingSystems
                                             where t.GetType().GetCustomAttribute(typeof(SystemsOrderAttribute)) is null
                                             select t).ToList();

            return outputSortedProcessingSystems.ToArray();
            /*foreach (Type t in assembly.GetTypes().Where(t => t.IsClass && t != typeof(BaseRenderingSystem) && typeof(BaseRenderingSystem).IsAssignableFrom(t)).ToList())
            {
                tempRendSys.Add((BaseRenderingSystem)t.GetConstructors()[0].Invoke(new object[0] { }));
            }*/

            //Манипуляции при помощи LINQ нужны для создания порядка работы систем на основании данных атрибута SystemsOrderAttribute
            //gameScene.systems = tList.ToArray();
            /*for (int i = 0; i < tempComps.Count; i++)
            {
                gameScene.componentsByType.Add(tempComps[i], (new List<BaseComponent>(), i));
            }*/

            //gameScene.eventManager = new EventsManager();


            

            /*foreach (BaseProcessingSystem s in tempProcessingSystems) //Инициализирует системы
            {
                s.gameScene = gameScene;
                s.Initialize();
            }

            foreach (BaseRenderingSystem s in tempRendSys)
            {
                s.gameWorld = gameScene;
                s.Initialize();
            }*/
        }

        /// <summary>
        /// Создаёт системы отрисовки и возвращает их в виде подготовленного словаря.
        /// </summary>
        /// <returns>
        /// Возвращает словарь, где в качестве ключа выступает перечисление этапа отрисовки, а в качестве значения - отсортированный по порядку итераций массив.
        /// </returns>
        public Dictionary<SystemsRenderingOrderEnum, BaseRenderingSystem[]> CreateRenderingSystems()
        {
            List<BaseRenderingSystem> tempRendSys = new List<BaseRenderingSystem>();
            Dictionary<SystemsRenderingOrderEnum, List<BaseRenderingSystem>> outputRenderingSystems = new Dictionary<SystemsRenderingOrderEnum, List<BaseRenderingSystem>>();

            for (int i = 0; i < loadedRenderingSystems.Count; i++)
            {
                tempRendSys.Add((BaseRenderingSystem)loadedRenderingSystems[i].Item2.GetConstructors()[0].Invoke(new object[0] { }));
            }

            foreach (SystemsRenderingOrderEnum s in Enum.GetValues(typeof(SystemsRenderingOrderEnum)))
            {
                outputRenderingSystems[s] = new List<BaseRenderingSystem>();
            }

            foreach (BaseRenderingSystem s in tempRendSys)
            {
                SystemsRenderingOrderAttribute tempAttr = s.GetType().GetCustomAttribute<SystemsRenderingOrderAttribute>();
                if (tempAttr != null) outputRenderingSystems[tempAttr.orderType].Add(s as BaseRenderingSystem);
            }

            Dictionary<SystemsRenderingOrderEnum, BaseRenderingSystem[]> outputSortedRenderingSystems = new Dictionary<SystemsRenderingOrderEnum, BaseRenderingSystem[]>();

            foreach (SystemsRenderingOrderEnum s in outputRenderingSystems.Keys)
            {
                outputSortedRenderingSystems.Add(s, outputRenderingSystems[s].OrderBy(sys => sys.GetType().GetCustomAttribute<SystemsRenderingOrderAttribute>()?.order ?? 0).ToArray());
            }

            return outputSortedRenderingSystems;
            /*Dictionary<SystemsRenderingOrderEnum, BaseRenderingSystem[]> tempSortedRenderingSystems = new Dictionary<SystemsRenderingOrderEnum, BaseRenderingSystem[]>();

            foreach (SystemsRenderingOrderEnum s in Enum.GetValues(typeof(SystemsRenderingOrderEnum)))
            {
                gameScene.renderingSystems[s] = outputRenderingSystems
                    [s]
                    .OrderBy(sys => sys.GetType().GetCustomAttribute<SystemsRenderingOrderAttribute>()?.order ?? 0)
                    .ToArray();
            }*/
        }

        /// <summary>
        ///Инициализирует массив, подготовленный для всех загруженных компонентов данных.
        /// </summary>
        /// <returns>
        ///Возвращает словарь заполненный элементами, ключи которых принадлежат массиву всех загруженных типов компонентов, а значения равны пустому списку компонентов и порядковый номер компонента. 
        /// </returns>
        public Dictionary<string, (List<BaseComponent>, int)> GetComponentsArray()
        {
            List<string> componentsList = new List<string>();

            for (int i = 0; i < systemsAssemblies.Count; i++)
            {
                foreach (Type currentType in systemsAssemblies[i].GetTypes())
                {
                    if (currentType.IsClass && currentType != typeof(BaseComponent))
                    {
                        if (typeof(BaseComponent).IsAssignableFrom(currentType))
                        {
                            componentsList.Add(currentType.FullName);
                        }
                    }
                }
            }

            Dictionary<string, (List<BaseComponent>, int)> outputComponents = new Dictionary<string, (List<BaseComponent>, int)>();

            for (int i = 0; i <  componentsList.Count; i++)
            {
                outputComponents.Add(componentsList[i], (new List<BaseComponent>() { }, i));
            }

            return outputComponents;
        }

        /// <summary>
        /// Инициализирует словарь с созданными экземплярами глобальных ячеек данных
        /// </summary>
        /// <returns>Возвращает словарь с созданными глобальными ячейками данных</returns>
        public Dictionary<string, GlobalDataContainer> GetGlobalDataContainersArray()
        {
            List<Type> dataContainersList = new List<Type>();

            for (int i = 0; i < systemsAssemblies.Count; i++)
            {
                foreach (Type currentType in systemsAssemblies[i].GetTypes())
                {
                    if (currentType.IsClass && currentType != typeof(GlobalDataContainer))
                    {
                        if (typeof(GlobalDataContainer).IsAssignableFrom(currentType))
                        {
                            dataContainersList.Add(currentType);
                        }
                    }
                }
            }

            Dictionary<string, GlobalDataContainer> outputDataContainers = new Dictionary<string, GlobalDataContainer>();

            for (int i = 0; i < dataContainersList.Count; i++)
            {
                outputDataContainers.Add(dataContainersList[i].FullName, (GlobalDataContainer)dataContainersList[i].GetConstructors()[0].Invoke(new object[0] { }));
            }

            return outputDataContainers;
        }

        /// <summary>
        /// Инициализирует словарь с созданными экземплярами локальных ячеек данных
        /// </summary>
        /// <returns>Возвращает словарь с созданными локальными ячейками данных</returns>
        public Dictionary<string, SceneDataContainer> GetSceneDataContainersArray()
        {
            List<Type> dataContainersList = new List<Type>();

            for (int i = 0; i < systemsAssemblies.Count; i++)
            {
                foreach (Type currentType in systemsAssemblies[i].GetTypes())
                {
                    if (currentType.IsClass && currentType != typeof(SceneDataContainer))
                    {
                        if (typeof(SceneDataContainer).IsAssignableFrom(currentType))
                        {
                            dataContainersList.Add(currentType);
                        }
                    }
                }
            }

            Dictionary<string, SceneDataContainer> outputDataContainers = new Dictionary<string, SceneDataContainer>();

            for (int i = 0; i < dataContainersList.Count; i++)
            {
                outputDataContainers.Add(dataContainersList[i].FullName, (SceneDataContainer)dataContainersList[i].GetConstructors()[0].Invoke(new object[0] { }));
            }

            return outputDataContainers;
        }

        /// <summary>
        /// Выгружает системы из списков.
        /// </summary>
        public void UnloadSystems()
        {
            loadedProcessingSystems = new List<(Assembly, Type)>();
            loadedRenderingSystems = new List<(Assembly, Type)>();
        }
    }
}
