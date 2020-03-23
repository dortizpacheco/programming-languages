# Seminario DSL-Reflection-Dynamic

## Lenguajes de Dominio Específico (DSL)

<!-- TODOÑ Cambiar los ejemplos a DSL de Persona -->

Supongamos la siguiente situación. Se quiere implementar un mecanismo
para crear maquinas de estado. A estas maquinas de estado debe ser
posible añadirles transiciones, entre estados, los cuales, seran
representados mediante _strings_. Una posible implementación para esto es
la siguiente:

```csharp
class StateMachine
{
    private List<Transition> transitions;

    public StateMachine()
    {
        this.transitions = new List<Transition>();
    }

    public AddTransition(Transition transition)
    {
        this.transitions.Add(transition);
    }
}

class Transition
{
    public string Origin { get; private set; }
    public string Dest { get; private set; }

    public Transition(string origin, string dest)
    {
        this.Origin = origin;
        this.Dest = dest;
    }
}
```

Luego podría crearse una máquina de estado de la siguiente manera:

```csharp
var machine = new StateMachine();
machine.AddTransition(new Transition("a", "b"))
machine.AddTransition(new Transition("b", "c"))
```

Lo anterior resuelve nuestro problema, pero que tal si quisieramos una manera mas
expresiva de expresar lo anterior.

```csharp
class StateMachine
{
    private List<Transition> transitions;
    private Transition newTransition;

    public StateMachine()
    {
        this.transitions = new List<Transition>();
        this.newTransition = null;
    }

    public Transition Transition()
    {
        this.newTransition = new Transition();
        this.Transitions.Add(this.newTransition);
        return this.newTransition;
    }
}

class Transition
{
    public string Origin { get; private set; }
    public string Dest { get; private set; }

    public Transition From(string origin)
    {
        this.Origin = origin;
        return this;
    }

    public Transition To(string dest)
    {
        this.Dest = dest;
        return this;
    }
}
```

Esto nos permitiria hacer lo siguiente:

```csharp
var machine = new StateMachine();
machine.Transition().From("a").To("b")
machine.Transition().From("b").To("c")
```

Lo que hemos hecho anteriormente es utilizar un **_DSL_** para representar nuestro
problema. A este tipo de interfaces o _apis_ que permiten llamados a métodos de forma
concatenada que mantienen estrecha relación entre sí, se les llama _Fluent API_, dado
que permiten una manera fluida de escribir el código, dando la sensación de ser un
lenguaje diferente.

<!-- TODO: Hablar mas de Fluent Api, maneras de implementarla (metodos extensores, etc.) -->

> Un **_DSL_** es un lenguaje de programacion de expresividad limitada, enfocado a un
> dominio especifico.

Analicemos la definicion anterior:

- **Lenguaje de programacion:**  
  Un **_DSL_** es utilizado para dar instrucciones a una computadora. Como
  cualquier otro lenguaje de programación, debe proveer una manera _legible_
  para los seres humanos de expresar lo que se desea, y aun asi premitir su
  ejecución en una computadora.

- **Naturaleza de lenguaje:**  
  Como lenguaje de programación, debe proveer fluidez, la expresividad no
  debe solo venir de las expresiones individuales, sino de la posibilidad de
  componer las mismas.

- **Expresividad limitada:**  
  Un lenguaje de programación de proposito general posee estructuras para
  controlar el flujo del programa y varias instrucciones complejas, estas
  pueden ser muy útiles, pero esto convierte al lenguaje en algo complejo de
  aprender, utilizar y entender. Un **_DSL_** por otro lado es facil de comprender,
  es legible, soporta solo lo necesario para ser utilizado en su dominio. No
  es posible contruir toda una aplicación con un **_DSL_**, se construyen
  componentes de la misma. No es turing-completo en la mayoría de los casos.

- **Enfocado a un dominio específico:**  
  Intuitivamente un lenguaje tan limitado solo puede ser útil si se conoce bien
  el dominio en el que será aplicado. Este enfoque en un dominio específico
  es lo que hace que un **_DSL_** tenga sentido ser utilizado.

**¿Que tal si tuvieramos lo siguiente en un archivo externo a la aplicación?**

```
state-machine:
    a -> b
    b -> c
```

Esta claro que es posible leer el contenido de este archivo y luego de un proceso de
_parsing_ convertirlo en una instancia de `StateMachine`. En este caso tambien
estaríamos en presencia de un **_DSL_**. Solo que este estaria ubicado en un archivo
externo a la aplicación principal.

Es posible dividir los **_DSL_** en tres grupos, de acuerdo a su naturaleza.

1. **DSLs externos:**  
   Un **DSL Externo** es un lenguaje diferente al de la aplicacion principal,
   generalmente con una sintaxis propia, aunque en ocaciones pueden
   utilizarse algunas existentes como _xml_, _json_, etc. Un script en un
   _dsl externo_ usualmente es parseado por la aplicacion principal.

2. **DSLs internos**:  
   Un **DSL Interno** es una manera particular de utilizar un lenguaje de
   proposito general. Un script en un _dsl interno_ es valido tambien en el
   lenguaje de proposito general, pero solamente utiliza un subconjunto del
   mismo en un estilo determinado para un aspecto del sistema general. El
   resultado se siente como un lenguaje diferente. **LISP** y **RUBY** son
   ejemplos importantes aqui.

3. **Language Workbenches:**  
   Un **Language Workbench** es un _IDE_ especializado para la creación de
   **_DSLs_**. En particular un **Language Workbench** no solo es utilizado
   para determinar la estructura del **_DSL_** sino para editar scripts de este
   **_DSL_**. EL _script_ resultante íntimamente combina el _IDE_ con el
   **_DSL_**

El primer caso que analizamos es un **_DSL_** _interno_ y el segundo uno _externo_.

### ¿Como distinguir un DSL?

Es facil ver que identificar si estamos en presencia de un **DSL** o no, es
algo que puede llegar a ser ambiguo, la frontera entre lo que es un **DSL** y
lo que no lo es es algo borrosa en muchos casos. Tratemos de definir mejor
esta frontera para cada tipo de **DSL**.

Para los **DSLs internos** esta frontera la define la expresividad limitada.
La diferencia aqui se establece entre el **DSL interno** y una _command query
api_. Una manera comun de documentar una _clase_ en una _api clásica_ es
listar los metodos que esta contiene y documentarlos uno a uno, estos metodos
tienen que tener sentido por si solos, ser _autocontenidos_; por otro lado en
un **DSL interno** podria pensarse en los metodos como palabras de un
lenguaje que no expresan una idea hasta que no son compuestas en oraciones.
Por ejemplo metodos `transition(event)` o `to(dest)` no tendrian sentido por
si solo en una _api clasica_, pero pueden ser utilizados en un **DSL interno**
de la siguiente manera `.transition(event).to(dest)`. Es común
tambien destinguir los **DSLs internos** por la no utilizacion de
condicionales, ciclos, o cualquier otra estructura de control de flujo.

Para los **DSLs externos** la frontera se establece con los lenguajes de
proposito general. Ser enfocados a un dominio no convierte a un lenguaje en
un **DSL**, dado que a pesar de su enfoque especifico, puede ser utilizado
para tareas de proposito general, por ejemplo lenguaes como **R**. Otro
ejemplo de un **DSL externo** son las expresiones regulares, estas definen un
lenguaje enfocado a hacer _text matching_, y ademas de tener este enfoque
especifico, tienen un lenguaje lo suficientemente _limitado_ como para lograr
solo la expresividad necesaria para resolver problemas de este dominio. Es
comun distinguir en los **DSL** que no sean _Turing completos_. Los archivos
de configuracion (_.json_, _.xml_, _etc_) pueden considerarse **DSL**, pero
solo en los casos en los que tienen el objetivo de ser no solo entendibles
por un ser humano, sino de ser editables.

### Modelo Semántico

Observese que en el caso del _DSL_ externo es posible construir luego del proceso
de **_parsing_** una instancia de `StateMachine` con el primer modelo, es decir:

```csharp
class StateMachine
{
    private List<Transition> transitions;

    public StateMachine()
    {
        this.transitions = new List<Transition>();
    }

    public AddTransition(Transition transition)
    {
        this.transitions.Add(transition);
    }
}

class Transition
{
    public string Origin { get; private set; }
    public string Dest { get; private set; }

    public Transition(string origin, string dest)
    {
        this.Origin = origin;
        this.Dest = dest;
    }
}
```

En este caso al modelo anterior le llamaremos **_modelo semántico_**, el
modelo es agnóstico al **_DSL_**, lo que nos brinda varias ventajas:

- Permite pensar en la semántica del modelo sin estar pensando simultáneamente
  en la sintaxis del **_DSL_**, esto es significativo dado que normalmente
  implementamos un **_DSL_** para representar situaciones bastante complejas.
- Permite testear de manera separada el _modelo_ y el **_DSL_**.
- Es posible tener múltiples **_DSL_** que utilicen el mismo modelo (incluyendo
  externos e internos).
- Es posible añadir funcionalidad al **_DSL_** y al _modelo_ de manera
  independiente

La existencia de un modelo semántico nos permite tambien tener varios **_DSL_**
para un mismo modelo semántico, permitiéndonos asi pensar en los **_DSL_** como
una mera interfaz al _modelo_. De cierta manera esta separación podría asemejarse
la separación entre _lógica_ y _visualización_ en cualquier otra aplicación.

**¿Es lo mismo Modelo Semantico que AST?**

Si estamos usando un _DSL externo_ y parseamos texto, obtendremos como sabemos
un _AST_ - En un _DSL interno_ la jerarquia del _AST_ la darian los llamados a
funciones -, la idea es convertir este _AST_ a nuestro **_Semantic Model_**, dado
que ejecutar sobre el **_AST_**, a pesar de ser posible, complijazaría mucho la
situación, debido a la atadura tan grande que se crearía entre la sintaxis del
**_DSL_** y la capacidad de procesamiento del mismo

**El _modelo semántico_ no se restringe solo a los _DSLs externos_:**

Es posible obtener las ventajas que nos brinda la existencia de un
_modelo semántico_ en un **_DSL_** _interno_. Para ello necesitamos separar
la capa del lenguaje de la capa del modelo, es aquí donde entran a jugar un papel
fundamental los _Expression Builders_, basicamente tendremos una capa en nuestro
modelo que proveerá el lenguaje y construir de la manera debida la instancia del
modelo que provee la funcionalidad.

<!-- TODO: Ejemplo de Expression Builder -->

### Ventajas y Desventajas de los DSL:

**Un _DSL_ tiene, entre otras, las siguientes evntajas:**

- **Aumento de la productividad:**  
  Menos bugs, más fácil de encontrarlos. Menos duplicación de código, gracias
  a modelo semántico.
- **Comunicación con expertos del dominio**
- **Mayor expresividad, el codigo tiene mayor semántica**
- **Modelo Computacional Alternativo:**  
  En ocaciones una máquina de estado o expresiones regulares, etc, pueden ser
  una mejor manera de lograr lo que se desea que la clásica programación
  imperativa, donde tenemos que tener en cuenta cada detalle para obtener el
  funcionamiento correcto de nuestro programa. En este caso el
  _modelo semántico_ representa esta forma computacional alternativa,
  mientras que un **DSL** permite instanciar y _construir_ este modelo de
  manera cómoda, con todas las ventajas que hemos visto hasta ahora.

**Por otro lado, tambien tiene inconvenientes:**

- **Cacofonía del lenguaje:**  
  El problema más común con los **_DSL_** es la gran cantidad de **_DSL_** que
  pueden llegar a existir, lo cual hace difícil que la gente los aprenda; y
  como al traer gente nueva a un proyecto, puede tomar tiempo que los nuevos
  trabajadores se adapten a los **_DSL_** que en este son usados.

### Ejemplos de DSL:

<!-- TODO:
- Hablar de LINQ:
  incluir generacion de código
- Hablar de C++ con las macros
- Poner ejemplos de Ruby y Lisp:
  Hablar de que características
  de estos lenguajes favorecen
  la creación de DSLs internos -->

## Dynamic

¿Qué tal si queremos un mayor dinamismo en nuestro _DSL_?

> Vease el codigo adjunto:  
> Implementacion en CSharp -> Carpeta DynamicDSl -> Clase Person

A continuación se explica como es posible lograr esto.

### Palabra clave `dynamic`

La palabra clave `dynamic` es utilizada para indicar que una instancia esta
involucrada en un _**enlace tardío** (Late Binding)_ y que el **_DLR_** o
_Dynamic Language Runtime_ se encargue del manejo de este objeto. El
comportamiento de este objeto durante el _enlace tardío_ puede ser controlado y
sobreescrito a traves de la implementación de la interfaz
`IDynamicMetaObjectProvider`, el **_DLR_** se encargará de llamar a los métodos
provenientes de `IDynamicMetaObjectProvider`, los cuales describen el comportamiento
de la clase en el momento de enlace.

### ¿Qué es el DLR?

Para entender que es el **_DLR_** debemos entender primero que es el **_CLR_**.

#### Common Language Rutime (CLR)

El _Common Language Runtime_ o _CLR_ es un entorno de ejecución para los códigos de los programas que corren sobre la plataforma _Microsoft .NET_. El _CLR_ es el encargado de compilar una forma de código intermedio (el conocido _IL_) a código de maquina nativo, mediante un compilador en tiempo de ejecución. No debe confundirse el _CLR_ con una máquina virtual, ya que una vez que el código está compilado, corre nativamente sin intervención de una capa de abstracción sobre el hardware subyacente.

La manera en que la máquina virtual se relaciona con el _CLR_ permite a los
programadores ignorar muchos detalles específicos del microprocesador que estará
ejecutando el programa. El _CLR_ también permite otros servicios importantes,
incluyendo los siguientes:

- Administración de la memoria
- Administración de hilos
- Manejo de excepciones
- Recolección de basura
- Seguridad

<!-- TODO: Diferencia entre maquina virtual y CLR -->

#### Dynamic Language Rutime (DLR)

El _Dynamic Language Rutime_ o _DLR_ agrega un conjunto de servicios al _CLR_ para
un mejor soporte de lenguajes dinámicos. Estos servicios incluyen lo siguiente:

- **Árboles de expresión:**  
  El _DLR_ utiliza árboles de expresión para representar la semántica del lenguaje.
  Para este propósito, el _DLR_ ha ampliado los árboles de expresión _LINQ_ para
  incluir el control de flujo, la asignación y otros nodos para modelar el lenguaje.
- **Interaccion y almacenamiento en caché:**  
  Mediante el dynamic call site, en un lugar en el código donde realiza una
  operación como `a + b` o `a.B()` en objetos dinámicos. El DLR almacena en caché
  las características `a` y `b` (generalmente los tipos de estos objetos) e
  información sobre la operación. Si dicha operación se ha realizado previamente, el
  DLR recupera toda la información necesaria de la memoria caché para un envío
  rápido.
- **Interoperabilidad dinámica de objetos**:  
  El DLR proporciona un conjunto de clases e interfaces que representan objetos y
  operaciones dinámicos y pueden ser utilizados por implementadores de lenguaje y
  autores de bibliotecas dinámicas. Estas clases e interfaces incluyen
  `IDynamicMetaObjectProvider`, `DynamicMetaObject`, `DynamicObject` y
  `ExpandoObject`.

### Enlace Tardío (Late binding)

**_Enlace:_** se le denomina a la asociación de una función con su objeto
correspondiente al momento de llamado de la misma.

**_Enlace de tiempo de compilación, estático o temprano:_** es el de una función
miembro, que se llama dentro de un objeto, dicho enlace se resuelve en tiempo de
compilación. Todos los métodos que pertenecen a un objeto o nombre de una clase
(estáticos) son a los que se pueden realizar enlaze de tiempo de compilación.

**_Enlace tardío o dinámico:_** es cuando solo se puede saber a que objeto pertenece
una función, en tiempo de ejecución. Uno de los ejemplos más comunes de este tipo
enlace son los metodos virtuales.

#### Enlace tardío con métodos dinámicos vs. métodos virtuales en CSharp

Los métodos virtuales todavía están "_vinculados_" en tiempo de compilación.
El compilador verifica la existencia real del método y su tipo de retorno, y el
fallará en compilar si el método no existe o existe alguna inconsistencia de tipos.

El método virtual permite el polimorfismo y una forma de enlace tardío, ya que el
método se enlaza al tipo adecuado en tiempo de ejecución, a través de la tabla de
métodos virtuales.

Por otro lado, con `dynamic`, no hay absolutamente ningún enlace en el momento de
compilación. El método puede o no existir en el objeto destino, y eso se
determinará en tiempo de ejecución.

Todo es una cuestión de terminología, `dynamic` es realmente un enlace tardío
(búsqueda del método de tiempo de ejecución), mientras que `virtual` proporciona el
envío del método de tiempo de ejecución a través de una búsqueda virtual, pero
todavía tiene algún "_enlace temprano_" en su interior.

### Como se logra el comportamiento dinámico en CSharp

Este comportamiento es concecuencia directa del desarrolo del _DLR_ el cual fue
concebido para admitir las implementaciones _"Iron"_ de los lenguajes de programación
_Python_ y _Ruby_ en _.NET_.

En en centro del entorno de ejecución _DLR_ se posiciona la clase
llamada DynamicMetaObject. Dicha clase implemeta los siguientes métodos para dar
respuesta a como actuar en todos los posibles esenarios en los que se puede
encontrar una instancia de un objecto en un momento dado:

- `BindCreateInstance`: crea o activa un objeto.
- `BindInvokeMember`: llamar a un método encapsulado.
- `BindInvoke`: ejecuta el objeto (como una función).
- `BindGetMember`: obtenga un valor de propiedad.
- `BindSetMember`: establece un valor de propiedad.
- `BindDeleteMember`: eliminar un miembro.
- `BindGetIndex`: obtener el valor en un índice específico.
- `BindSetIndex`: establece el valor en un índice específico.
- `BindDeleteIndex`: elimina el valor en un índice específico.
- `BindConvert`: convierte un objeto a otro tipo.
- `BindBinaryOperation`: invoque un operador binario en dos operandos suministrados.
- `BindUnaryOperation`: invoque un operador unario en un operando suministrado.

De manera general las clases definidas de manera ordinaria (estática) saben como
reaccionar en dichos esenarios. Pero las clases _dinámicas_ no tienen estas
reacciones predefinidas por lo cual es necesario predefinir para estas clases su
propio `DynamicMetaObject`, el cual en tiempo de ejecución sepa que tiene que
ejecutar en cada esenario. Para definir una clase _dinámica_, `System.Dynamic`
proveé la interfaz `IDynamicMetaObjectProvider`, la cual contiene el metodo:

```csharp
DynamicMetaObject GetMetaObject(Expression parameter)
```

El cual debe encargarse de retornar el `DynamicMetaObject` que describa el
comportamiento de la clase _dinamica_ que implemeta la interfaz
`IDynamicMetaObjectProvider` según el árbol de expresiones que dicho método recibe
como parámetro

### `System.Dynamic.DynamicObject`

Como se puede observar, en principio lograr un comportamieto dinámico en **C#** pasa
por crear estos `DynamicMetaObject` y tener conocimientos para trabajar sobre el
árbol de expresiones de **C#**. Para evitar todo este proceso `System.Dynamic`
provée la clase `DynamicObject`, pensada para poder definir comportamietos dinámicos
abstraídos de todo el proceso anteriormente descrito pues ya cuenta con una
implemetacion del metodo `GetMetaObject` de `IDynamicMetaObjectProvider`. Dicha
imlpemetación relaciona los siguentes métodos a sus respectivos esenarios:

- ```csharp
  public virtual bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result);
  ```

  Proporciona implementación para operaciones binarias. Las clases derivadas de la
  clase `DynamicObject` pueden sobreescribir este método para especificar el
  comportamiento dinámico para operaciones como la suma, multiplicación, etc. La
  clase BinaryOperationBinder contiene una `ExpressionType` con información de la
  operación que se realiza en el momento de llamado de esta función. Este método
  considera que la instancia de la clase derivada de `DynamicObject` es el operador
  de la derecha y _arg_ es el de la izq.

- ```csharp
  public virtual bool TryConvert(ConvertBinder binder, out object result);
  ```

  Proporciona implementación para operaciones de conversión de tipos. Las clases
  derivadas de la clase `DynamicObject` pueden sobreescribir este método para
  especificar el comportamiento dinámico de las operaciones que convierten un objeto
  de un tipo a otro. La clase `ConvertBinder` contiene información sobre del tipo al
  cual se esta tratando de hacer la conversión e incluso contiene información sobre
  si el proceso es explícito o implícito.

- ```csharp
  public virtual bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result);
  public virtual bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value);
  ```

  Proporciona la implementación para operaciones que obtienen (establecen) valores
  de miembros por índices. Las clases derivadas de la clase `DynamicObject` pueden
  sobreescribir este método para especificar el comportamiento dinámico para
  operaciones tales como obtener (establecer) un valor para una propiedad mediante
  unos índices específico.

- ```csharp
  public virtual bool TryGetMember(GetMemberBinder binder, out object result);
  public virtual bool TrySetMember(SetMemberBinder binder, object value);
  ```

  Proporciona la implementación para operaciones que obtienen (establecen) valores
  de miembros. Las clases derivadas de la clase `DynamicObject` pueden sobreescribir
  este método para especificar el comportamiento dinámico para operaciones tales
  como obtener (establecer) un valor de una propiedad. La clase `GetMemberBinder`
  (`SetMemberBinder`) contiene información sobre el nombre de la propiedad en
  cuestión.

- ```csharp
  public virtual bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result);
  ```
  Proporciona la implementación para operaciones que invocan a un miembro. Las
  clases derivadas de la clase `DynamicObject` pueden sobreescribir este método para
  especificar el comportamiento dinámico para operaciones como llamar a un método.
  La clase `InvokeMemberBinder` contiene información sobre el nombre del miembro en
  cuestión y `args` es un array con los parámetros.

* ```csharp
  public virtual bool TryInvoke(InvokeBinder binder, object[] args, out object result);
  ```
  Proporciona la implementación para operaciones que invocan un objeto. Las clases
  derivadas de la clase `DynamicObject` pueden sobreescribir este método para
  especificar el comportamiento dinámico para operaciones como invocar un objeto o
  un delegado. La clase `InvokeBinder` contiene informacion sobre la cantidad de
  argumentos y sus nombres mediante una propiedad del tipo `CallInfo` y `args` son
  los parámetros.

### `System.Dynamic.ExpandoObject`

Aunque la clase DynamicObject es una gran abstración del proceso base, para su
utilización es necesario implementar una clase que herede de esta y realice los
override necesarios, lo cual es demasiado verboso en los casos más sencillos.
Suponiendo que solo se necesita de un objeto dinámico que permita un control
dinámico de propiedades, mediante `DynamicObject` necesitamos implementar los
metodos `TryGetMember` y `TrySetMember`. Para evitar esto `System.Dynamic` proveé la
clase `ExpandoOject`, la misma es una clase `sealed` y por tanto no se puede
extender.

`ExpandoObject` implementa las interfaces
`IDictionary<KeyValuePair<string, object>>` y `IDynamicMetaObjectProvider` entre
otras. Mediante las dos interfaces antes mencionadas dicha clase logra el manejo
dinámico de las propiedades. El proceso es realmente sencillo puesto que en su
interior contiene algún tipo de implementación de diccionario, al momento de asignar
una propiedad guarda el nombre de la propiedad como llave y el objeto que se le esta
asignando como valor. Y en el momento en que se hace referencia a una propiedad
busca si el nombre de dicha propiedad se encuentra entre las llaves, en caso
afirmativo se devuelve el valor correspondiente, de lo contrario se lanza un
excepción.

## Reflection

<!-- TODO: Añandir una pequeña explicacion de Reflection en CSharp -->
