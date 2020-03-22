# Seminario DSL-Reflection-Dynamic

## Lenguajes de Dominio Específico (DSL)

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

    public StateMachine()
    {
        this.transitions = new List<Transition>();
        this.newTransition = null;
    }

    public Transition Transition()
    {
        newTransition = new Transition();
        this.Transitions.Add(newTransition);
        return newTransition;
    }
}

class Transition
{
    public string Origin { get; private set; }
    public string Dest { get; private set; }

    public Transition From(string origin)
    {
        this.Origin = origin;
        return This;
    }

    public Transition To(string dest)
    {
        this.Dest = dest;
        return This;
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
modelo que proveerá el lenguaje y construir de la manera debida la instancia del modelo que provee la funcionalidad.

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
