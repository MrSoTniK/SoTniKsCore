# Ядро основано на:
- ECS (LeoECS) 
https://github.com/Leopotam/ecs#%D0%9A%D0%BE%D0%BC%D0%BF%D0%BE%D0%BD%D0%B5%D0%BD%D1%82
- Unity Ecs implementation (UniLeo)
https://github.com/voody2506/UniLeo
- DI (Zenject)
https://github.com/matterport/Zenject

## Описание классификации скриптов в папке Game

<details>
<summary>Подробнее</summary>
  
### /Components
Обычные структуры, которые используются в EcsFilter. Под обычными понимаются компоненты, содержащие поля.
### /Data
Классы, содержащие данные, которые не меняются в течение игровой сессии.
### /Enums
Типы-перечисления enum
### /Factories
Наследники классов, которые лежат в Assets/Scripts/Core/Factories/ (реализуют создание новых объектов, паттерн factory)
### /Info
Классы или структуры, содержащие данные, которые меняются в течение игровой сессии.
### /Installers
Классы – наследники MonoInstaller класса.
### /Providers
Провайдеры для структур, которые используются в EcsFilter.
### /Requests
Структуры, которые используется в EcsFilter, но обозначающие какой-либо запрос на действие, которое должно быть осуществлено соответствующей системой (например, JumpRequest, ShootRequest, MakeNewObjectRequest и т.д.).
### /ScriptableObjects
Классы-наследники DataBaseAbstract или просто наследники ScriptableObject.
### /Startups
Классы – наследники EcsSceneStartup.
### /Systems
Классы, реализующие интерфейсы IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem.
### /Tags
Структуры, которые используется в EcsFilter, но у которых нет каких-либо полей и которые служат чисто в качестве маркера игрового объекта.
### /Views
Классы-наследники ViewBase или MonoBehaviour.
  
</details>

## Описание ядра (Core) архитектуры

<details>
<summary>Подробнее</summary>

Основные классы архитектуры содержатся в папке Core. Производные от этих классов или какие-либо не связанные с архитектурой скрипты содержатся в папке Game.
Основная логика работы содержится в Assets/Scripts/Core/Infrastructure
Логика работы проекта состоит из классов, которые работают во всём проекте, и классов, которые работают в рамках конкретных сцен (то есть у каждой сцены есть свои скрипты с логикой).
В качестве фундамента построения архитектуры используется Dependency Injection, реализуемый в Zenject’е. Для работы данного фреймворка используются:
- __SceneContext__ - скрипт, который должен висеть на GameObject с таким же именем для сцен (префабы с данным скриптом в Assets/Prefabs/Contexts/).
- __ProjectContext__ - скрипт, префаб с которым должен находиться в папке Resources (находится в Assets/Settings/Resources/).
На префабы контекстов (сцены или проекта) в поле массива MonoInstallers помещаются наследники от класса MonoInstaller, в которых содержатся те классы, которые помещаются в контейнер.

### Принцип работы
Принцип работы состоит в том, что:
1) Для каждой сцены и проекта в частности биндятся и инициализируются классы с информацией о сцене через наследников SceneInfoAbstract (один на проект и по одному на каждую сцену). В SceneInfoAbstract есть поле generic-типа, отвечающее за тип сцены (уникальный индекс). По умолчанию реализовано в Assets/Scripts/Game/Enums/SceneType (у каждой сцены должен быть уникальный тип). 
2) Затем биндится и инициализируется класс WorldsInfo, который содержит Dictionary с int-ключом и EcsWorld-значением. Затем создаётся экземляр EcsWorld и добавляется в словарь по уникальном ключу, который берётся с поля наследника SceneInfoAbstract (приведение enum к int методом  Convert.ToInt32(SceneType type)). 
3) После этого создаётся и биндятся все системы (классы, реализующие интерфейсы IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem).
4) Далее создаётся экземпляр наследника EcsSceneStartup, в котором реализуется работа всех систем, принадлежащих конкретному экземпляру EcsWorld сцены или проекта.

### Assets/Scripts/Core/Infrastructure/Installers

### /Bootstrap
__BootstrapSceneInstaller__
- Абстрактный класс для создания и инициализации основного класса LeoECS EcsWorld, наследники класса должны создавать и биндить наследников класса EcsSceneStartup.
BootstrapSceneInstaller требует поле с наследником MonoInstaller, в котором забиндены классы-системы.
__BootstrapSceneInstaller__
- Временно не используется, но его использование возможно, если планируется только одна сцена на весь проект. Все те же функции, что и у BootstrapSceneInstaller.
### /Components
__ComponentsInstaller__
- Реализует поиск и конвертацию в Entities всех структур, "обёрнутых" в MonoProvider. Используется только для сцен.
### /Controllers
__ControllersInstaller__
- В наследниках этого класса биндятся классы-контроллеры, которые обрабатывают различные события (events).
### /Data
__DataInstaller__
- В наследниках этого класса биндятся файлы, содержащие какие-либо числовые данные и наследники SceneInfoAbstract, тип сцены задётся через поле SceneType.
### /DataBases
__DataBasesInstaller__
- В наследниках этого класса биндятся ScriptableObjects, которые выступают в роли баз данных.
### /Factories
__FactoriesSceneInstaller__
- В наследниках этого класса биндятся классы-заводы, которые создают новые экземпляры игровых объектов.
### /Systems
__SystemsInstaller__
- В наследниках этого класса биндятся классы-системы (реализующие интерфейсы IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem).
### /Views
__ViewsInstaller__
- В наследниках этого класса биндятся все компоненты наследники ViewBase, который наследуется от MonoBehaviour.
### /World
__WorldInstallerAbstract__
- Абстрактный класс, который создаёт экзепляр EcsWorld и помещает его в Dictionary WorldsInfo по ключу, конвертируемого от поля SceneType наследника SceneInfoAbstract.
__WorldsInfoInstaller__
- Один на проект!!!  Создаёт экземпляр и биндит WorldsInfo.

### Установленный порядок следования инсталлеров в Scene- или Project- Context'ах
### для ProjectContext:
- DataBasesProjectInstaller (наследник DataBasesInstaller)
- DataProjectInstaller (наследник DataInstaller)
- WorldsInfoInstaller
- ProjectWorldInstaller (наследник WorldInstallerAbstract)
- ToolsInstaller (наследник MonoInstaller для каких-либо классов в которых реализованы методы со сторонней логикой, например класс Randomizer)
- SystemsProjectInstaller (наследник SystemsInstaller)
- BootstrapInstaller (наследник BootstrapSceneInstaller)

### для SceneContext (например, для сцены Game, Assets/Prefabs/Contexts/GameSceneContext.prefab):
- GameDataBasesInstaller (наследник DataBasesInstaller)
- GameDataInstaller (наследник DataInstaller)
- GameSceneWorldInstaller (наследник WorldInstallerAbstract)
- ComponentsGameInstaller (наследник ComponentsInstaller)
- GameFactoriesInstaller (наследник FactoriesSceneInstaller)
- GameViewsInstaller (наследник ViewsInstaller)
- GameSystemsInstaller (наследник SystemsInstaller)
- GameBootstrapInstaller (наследник BootstrapSceneInstaller)

__EcsGameStartup__
- Класс, реализующий работу классов-систем проекта (!!!временно не используется, но его исопльзование возможно, если одна сцена на весь проект!!!)
Получается логика: один Awake, Start, Update, FixedUpdate (методы MonoBehaviour) на проект.

__EcsSceneStartup__
- Класс, наследники которого реализуют работу классов-систем проекта.
Получается логика: один Awake, Start, Update, FixedUpdate (методы MonoBehaviour) на сцену. 

__RxField__
- Класс, в котором осуществляется контроль над сменой значения экземпляра generic типа T.

### Core/Infrastructure/Controllers
- Здесь содержатся абстрактные классы, в которых прописана структура работы с ивентами.
### Core/Infrastructure/Components
- Здесь находятся интерфейсы для различных видов структур компонент, используемых в LeoEcs.
### Core/Data
__DataAbstract__
- Класс, от которого могут наследоваться классы, содержащие данные в виде числовых значений или каких-либо других данных (типо экземпляров классов).
### Core/Extensions
- Здесь лежат расширения в виде новых методов для классов плагинов или packages.
### Core/Factories
__FactoryAbstract__
- Содержит классы для создания экземпляров игровых объектов, темплейты которых берутся из баз данных.
### Core/ScriptableObjects
__DataBaseAbstract__
- Абстрактный класс для создания базы данных с методами выбора её элемента.
### Core/Tools
__Назначение__
- Место хранения классов, выступающих в качестве вспомогательных помощников. Например, рандомайзера, загрузчика новых сцен.
### JsonManager
- Статический класс для загрузки или сохранения через использование json-файлов
### Randomizer
- Класс для получения случайных интовых значений
### ScenesLoader
- Статический класс для загрузки-выгрузки игровых сцен
(!!!опционально!!!, не особо используется, но может пригодиться)
### WorldGetter
- Статический класс для получения экземпляра EcsWorld
### WorldMessageSender
- Статический класс для добавления новых Entities в экземпляр класса EcsWorld
### Core/Views
__ViewBase__
- Класс-наследник MonoBehaviour для игровых объектов, в которых необходимо использование методов, не входящих в логику работы с Ecs, например, физические взаимодействия, реализуемых посредством методов OnTriggerEnter, OnTriggerExit.

### (опционально)
__InitializeViewRequest__
- Реквест для инициализации поля типа EcsEntity

__InitializeViewRequestProvider__
- Провайдер реквеста

__ViewsEntityInitializingSystem__
- Система, реализующая логику инициализации
 
  </details>
  
