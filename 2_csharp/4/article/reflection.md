## Reflection

Reflection es la capacidad de un proceso de examinar, introspectar y modificar su propia estructura y comportamiento. Reflection ayuda a los programadores a crear bibliotecas de software genéricas para mostrar datos, procesar diferentes formatos de datos, realizar la serialización o deserialización de datos para la comunicación, o agrupar y desagrupar datos para contenedores o ráfagas de comunicación. Reflection hace que un lenguaje sea más adecuado para el código orientado a la red.

Tambien se puede utilizar para observar y modificar la ejecución del programa en tiempo de ejecución. Esto generalmente se logra mediante la asignación dinámica de código de programa en tiempo de ejecución. En lenguajes de programación orientados a objetos, esta tecnica permite la inspección de clases, interfaces, campos y métodos en tiempo de ejecución sin conocer los nombres de las interfaces, campos y métodos en tiempo de compilación. También permite la creación de instancias de nuevos objetos y la invocación de métodos. Por lo antes dicho es claro que es tambien una estrategia clave para la metaprogramación .



### Reflection en C#

Reflection en C# tiene como clase principal **System.Type**; clase abstracta que representa un tipo en el Common Type System (CTS) lo cual representa una de las principales componentes de CLR. Cuando utiliza esta clase, puede encontrar los tipos utilizados en un módulo y un espacio de nombres y también determinar si un tipo dado es una referencia o un tipo de valor. Puede analizar las tablas de metadatos (Campos, Propiedades, Métodos, Eventos) correspondientes para ver estos elementos

El enlace tardío también se pueden lograr a través de la Reflection. Un ejemplo claro: es posible que no se sepa qué ensamblaje cargar durante el tiempo de compilación. En este caso, puede pedirle al usuario que ingrese el nombre y el tipo del ensamblado durante el tiempo de ejecución para que la aplicación pueda cargar el ensamblaje apropiado. Con el tipo System.Reflection.Assembly, tiene algunos metodos estáticos que le permiten cargar un ensamblaje directamente: LoadFrom, LoadWithPartialName

Dentro del archivo PE (ejecutable portátil) hay principalmente metadatos, que contienen una variedad de tablas diferentes, como:
Tabla de definición archivada, Tabla de definición de tipo, Tabla de definición de método

### Api se System.Reflection

Para realizar la manipulación de una instacia de un objeto en C# mediante Reflection se necesita comenzar obteniendo la clase **System.Type** que describe dicha instancia, **System.Type** es la puerta principal para el uso de Reflection en C#. Para abrir esta puerta C# nos brindan tres formas principales: **_typeof_**, **_object.GetType()_**, **_Type.GetType_** sus sintaxis respectivas son las siguientes:

``` csharp
    Type t = typeof(Person);
```
``` csharp
    var person = new Person();
    Type t = person.GetType(); 
```
``` csharp
    Type t = Type.GetType("CSharp.DynamicReflectionDSL.Person");
```
Aunque las formas antes descritas depende de conocer la tipo del objeto que queremos modificar de antemano. Para superar esta limitacion dentro de **System.Reflection** tenemos tambien la clase **Assambly** la cual contiene el metodo estático **_GetExecutingAssembly()_** que retorna una instancia del ensamblado que se encuentra en ejecución. El cual contiene todos lo tipos en el interior del mismo y por tanto se puede realizar una busqueda entre estos tipos:
    
``` csharp
        Type[] classes = Assembly.GetExecutingAssembly().GetTypes();
        foreach (var type in classes) 
            if (type.Name == "Nombre del objeto") {....} 
```

Una vez ya se tiene la instancia de **System.Type** necesaria, la misma contienen una serie de metodos para interactuar con las clases de **System.Reflection** entre los cuales resaltamos:

- **_GetEvent_** y **_GetEvents_** los cuales retornan los **_EventInfo_** de la instancia en cuestión
- **_GetField_** y **_GetFields_** los cuales retornan los **_FieldInfo_** de la instancia en cuestión
- **_GetMember_** y **_GetMembers_** los cuales retornan los **_MemberInfo_** de la instancia en cuestión
- **_GetProperty_** y **_GetPropertys_** los cuales retornan los **_PropertyInfo_** de la instancia en cuestión
- **_GetMethod_** y **_GetMethods_** los cuales retornan los **_MethodInfo_** de la instancia en cuestión
- **_GetEvent_** y **_GetEvents_** los cuales retornan los **_EventInfo_** de la instancia en cuestión
- **_GetConstructorInfo_** y **_GetConstructorInfos_ ** los cuales retornan los **_ConstructorInfo_** de la instancia en cuestión

Una vez se alcanza estas clases de **System.Reflection** solo necesitamos conocer sobre cada uno de ellos para poder usar sus distintos metodos para indagar y modificar sus metadatos. A continuación se exponen la descripción de algunos de ellos :

- **EventInfo** : clase abstracta. Contiene información sobre un evento específico;
- **FieldInfo** : clase abstracta. Puede contener información sobre los miembros de datos especificados de la clase;
- **MemberInfo**: clase abstracta. Contiene información de comportamiento general para clase EventInfo , FieldInfo , MethodInfo y PropertyInfo ;
- **MethodInfo** : clase abstracta. Contiene información sobre el método especificado;
- **ParameterInfo** : clase abstracta. Contiene información sobre un parámetro dado en un método dado;
- **PropertyInfo** : clase abstracta. Contiene información sobre la propiedad especificada.
- **ConstructorInfo**: clase abstracta. Contiene datos sobre los parámetros, modificadores de acceso y detalles de implementación de un constructor.

Cada uno de ellos tienen distintos metodos para acceder a los metadatos de manera diferente y de acuerdo con sus respectivos significados. En particular ejemplificamos el caso de **PropertyInfo**:

    
``` csharp
    ...
    var person = new Person();
    person.LastName = "Name"
    PropertyInfo info = .GetProperty("LastName");
    info.GetType(); //System.Reflection.PropertyInfo
    a.GetValue(person); //Name   
    a.SetValue(person,"NewName");//person.LastName = "NewName"
    a.Name;//LastName
    a.PropertyType; //string
    ...
```