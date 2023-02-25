# My core which is based on:
- ECS (LeoECS) 
https://github.com/Leopotam/ecs#%D0%9A%D0%BE%D0%BC%D0%BF%D0%BE%D0%BD%D0%B5%D0%BD%D1%82
- Unity Ecs implementation (UniLeo)
https://github.com/voody2506/UniLeo
- DI (Zenject)
https://github.com/matterport/Zenject

## Описание классификации классов в папке Game

/Components
Обычные структуры, которые используется в EcsFilter. Под обычными понимаются компоненты, содержащие поля.
/Controllers
Классы, подписывающиеся на события и обрабатывающие взаимодействие с этими событиями (events).
/Data
Классы, содержащие данные, которые не меняются в течение игровой сессии.
/Info
Классы, содержащие данные, которые меняются в течение игровой сессии.
/Installers
Классы – наследники MonoInstaller класса.
/Providers
Провайдеры для структур, которые используются в EcsFilter.
/Requests
Структуры, которые используется в EcsFilter, но обозначающие какой-либо запрос на действие, которое должно быть осуществлено соответствующей системой (например, JumpRequest, ShootRequest, MakeNewObjectRequest и т.д.).
/Tags
Структуры, которые используется в EcsFilter, но у которых нет каких-либо полей и которые служат чисто в качестве маркера игрового объекта.
/Scenes
Классы – наследники EcsSceneStartup.
/ScriptableObjects
Классы наследники DataBaseAbstract или просто наследники ScriptableObject.
/Systems
Классы, реализующие интерфейсы IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem.
/Views
Классы-наследники ViewBase или MonoBehaviour.

## Описания ядра (Core) архитетуры
Основные классы архитектуры содержатся в папке Core. Производные от этих классов или какие-либо не связанные с архитектурой скрипты содержатся в папке Game.
Основная логика работы содержится в Core/Infrastructure
Логика работы проекта состоит из классов, которые работают во всём проекте и классов, которые работают в рамках конкретных сцен (то есть у каждой сцены есть свои скрипты с логикой).
В качестве фундамента построения архитектуры используется Dependency Injection, реализуемый в Zenject’е. Для работы данного фреймворка используются классы 
- SceneContext, который должен висеть на GameObject с таким же именем для сцен.
-ProjectContext, прфеаб с которым должен находится в папке Resources.
В поле со списком Mono Installers помещаются скрипты, классы которых являются наследниками класса MonoInstaller.
Core/Infrastructure/Installers
/Bootstrap
BootstrapInstaller
Создаёт и инициализирует основной класс LeoECS EcsWorld, создаёт и биндит различные классы-инструменты (Core/Tools) и EcsGameStratup.
BootstrapSceneInstaller
Создаёт поле основного класса-наследника EcsSceneGameStratup, экземпляр которого создаётся и биндится в наследниках BootstrapSceneInstaller
Оба класса требуют поле с наследником MonoInstaller, в котором забиндены классы-системы.
/Components
ComponentsInstaller
В наследниках этого класса биндятся (помещаются в контейнер) структуры-компоненты, которые используются в LeoECS.
/Controllers
ControllersInstaller
В наследниках этого класса биндятся классы-контроллеры, которые обрабатывают различные события (events).
/Data
DataInstaller
В наследниках этого класса биндятся файлы, содержащие какие-либо числовые данные.
/DataBases
DataBasesInstaller
В наследниках этого класса биндятся ScriptableObjects, которые выступают в роли баз данных.
/Factories
FactoriesSceneInstaller
В наследниках этого класса биндятся классы-заводы, которые создают новые экземпляры игровых объектов
/Systems
SystemsInstaller
В наследниках этого класса биндятся классы-системы (реализующие интерфейсы IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem)
/Views
ViewsInstaller
В наследниках этого класса биндятся все компоненты наследники ViewBase, который наследуется от MonoBehaviour.

Установленный порядок следования инсталлеров в Scene- или Project- Context'ах
FactoriesInstaller
DataBasesInstaller
DataInstaller
ControllersInstaller
SystemsInstaller
BootstrapInstaller или BoorstrapSceneInstaller

EcsGameStartup
Класс, реализующий работу классов-систем проекта.
Получается логика: один Awake, Start, Update, FixedUpdate (методы MonoBehaviour) на проект. 
EcsSceneStartup
Класс, наследники которого реализуют работу классов-систем проекта.
Получается логика: один Awake, Start, Update, FixedUpdate (методы MonoBehaviour) на сцену. 

RxField
Класс, в котором осуществляется контроль над сменой значения экземпляра generic типа T.

Core/Infrastructure/Controllers
Здесь содержатся абстрактные классы, в которых прописана структура работы с ивентами.
Core/Infrastructure/Components
Здесь находятся интерфейсы для различных видов структур компонент, используемых в LeoEcs.
Core/Data
DataAbstract
Класс, от которого могут наследоваться классы, содержащие данные в виде числовых значений или каких-либо других данных (типо экземпляров классов).
Core/Extensions
Здесь лежат расширения в виде новых методов для классов плагинов или packages.
Core/Factories
FactoryAbstract
Содержит классы для создания экземпляров игровых объектов, темплейты которых берутся из баз данных.
Core/ScriptableObjects
DataBaseAbstract
Абстрактный класс для создания базы данных с методами выбора её элемента.
Core/Tools
Место хранения классов, выступающих в качестве вспомогательных помощников. Например, рандомайзера, загрузчика новых сцен.
Core/Views
ViewBase
Класс-наследник MonoBehaviour для игровых объектов, в которых необходимо использование методов, не входящих в логику работы с Ecs, например, физические взаимодействия, реализуемых посредством методов OnTriggerEnter, OnTriggerExit.
InitializeViewRequest
Реквест для инициализации поля типа EcsEntity
InitializeViewRequestProvider
Провайдер реквеста
ViewsEntityInitializingSystem
Система, реализующая логику инициализации

