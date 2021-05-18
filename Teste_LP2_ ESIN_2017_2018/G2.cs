using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste_LP2__ESIN_2017_2018
{
    public enum LOCAL { braga, porto, lisboa, alentejo, faro, ilhas }
    public enum TYPE { sports, culture, photography, science }
    public enum STATE { recent, done, delayed, cancelled }

    class Event
    {
        #region Attributes

        static int idCounter = 0;
        int id;
        DateTime date;
        string name;
        LOCAL local;
        TYPE type;
        STATE state;

        #endregion

        #region Constructors
        public Event() { }

        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value;}
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public LOCAL Local
        {
            get { return local; }
            set { local = value; }
        }
        public TYPE Type
        {
            get { return type; }
            set { type = value; }
        }
        public STATE State
        {
            get { return state; }
            set { state = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) return false;
            else
            {
                Event a = (Event)obj;
                return (String.Compare(a.Name, Name) == 0 && a.Local == Local
                && a.Date.CompareTo(Date) == 0 && a.State == State && a.Type == Type);
            }
            
        }
        #endregion

        #region Operators
        /// <summary>
        /// Operador == para comparar 2 eventos
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Event a, Event b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Operador != para comparar 2 eventos
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Event a, Event b)
        {
            return !(a == b);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Cria um evento e verifica se o mesmo pode ser criado
        /// </summary>
        /// <returns></returns>
        public static Event CreateEvent()
        {
            Event e = new Event();
            int aux;

            //Insert Name
            Console.Write("Name of the event: ");
            e.Name = Console.ReadLine();

            while(e.Name.Length <= 2)
            {
                Console.Clear();
                Console.Write("Name of the event: ");
                e.Name = Console.ReadLine();
            }

            //Insert Date
            Console.Clear();
            Console.Write("Date of the event: ");
            e.Date = DateTime.Parse(Console.ReadLine());

            while ((e.Date.CompareTo(DateTime.MaxValue) > 0 || e.Date.CompareTo(DateTime.MaxValue) < 0)
                && e.Date.Year.CompareTo(2019) != 0)
            {
                Console.Clear();
                Console.Write("Date of the event: ");
                e.Date = DateTime.Parse(Console.ReadLine());
            }

            //Insert Local
            Console.Clear();
            Console.Write("Local of the event:\n[1] - Braga\n[2] - Porto\n[3] - Lisboa\n[4] - Alentejo\n[5] - Faro\n[6] - Ilhas\nOPTION ==> ");
            aux = int.Parse(Console.ReadLine());

            while (aux < 1 || aux > 6)
            {
                Console.Clear();
                Console.Write("Local of the event:\n[1] - Braga\n[2] - Porto\n[3] - Lisboa\n[4] - Alentejo\n[5] - Faro\n[6] - Ilhas\nOPTION ==> ");
                aux = int.Parse(Console.ReadLine());
            }

            if (aux == 1) e.Local = LOCAL.braga;
            else if (aux == 2) e.Local = LOCAL.porto;
            else if (aux == 3) e.Local = LOCAL.lisboa;
            else if (aux == 4) e.Local = LOCAL.alentejo;
            else if (aux == 5) e.Local = LOCAL.faro;
            else if (aux == 6) e.Local = LOCAL.ilhas;

            //Insert Type of event
            Console.Clear();
            Console.Write("Type of the event:\n[1] - Sports\n[2] - Culture\n[3] - Photography\n[4] - Science\nOPTION ==> ");
            aux = int.Parse(Console.ReadLine());

            while (aux < 1 || aux > 4)
            {
                Console.Clear();
                Console.Write("Type of the event:\n[1] - Sports\n[2] - Culture\n[3] - Photography\n[4] - Science\nOPTION ==> ");
                aux = int.Parse(Console.ReadLine());
            }

            if (aux == 1) e.Type = TYPE.sports;
            else if (aux == 2) e.Type = TYPE.culture;
            else if (aux == 3) e.Type = TYPE.photography;
            else if (aux == 4) e.Type = TYPE.science;

            //Event state + Event ID
            e.State = STATE.recent;
            e.Id = ++idCounter;

            return e;
        }
        /// <summary>
        /// Mostra informacao de um evento
        /// </summary>
        /// <param name="e"></param>
        public static void ShowEvent(Event e)
        {
            Console.WriteLine("Name: {0}\nDate: {1}\nType: {2}\nLocal: {3}\nState: {4}", e.Name, e.Date, e.Type, e.Local, e.State);
        }
        /// <summary>
        /// Atualiza estado do evento
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool UpdateEventState(Event e)
        {
            int aux;

            Console.Write("Choose the new state of the event:\n[1] - Cancelled\n[2] - Delayed\nOPCAO ==> ");
            aux = int.Parse(Console.ReadLine());

            while (aux != 1 && aux != 2)
            {
                Console.Clear();
                Console.Write("Choose the new state of the event:\n[1] - Cancelled\n[2] - Delayed\nOPCAO ==> ");
                aux = int.Parse(Console.ReadLine());
            }

            if (aux == 1) e.State = STATE.cancelled;
            else if (aux == 2) e.State = STATE.delayed;

            return true;
        }

        #endregion
    }

    class Events
    {
        #region Attributes

        static List<Event> events;

        #endregion

        #region Constructors

        static Events()
        {
            events = new List<Event>();
        }

        #endregion

        #region Methods
        public static void ShowEvents(List<Event> es)
        {
            foreach(Event e in es)
            {
                Event.ShowEvent(e);
            }
        }

        /// <summary>
        /// Metodo que verifica se evento ja existe ou se na data do evento 'e'
        /// já tem algum evento nesse horario desse tipo
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool VerifyEventCanBeCreated(Event e)
        {
            foreach(Event aux in events)
            {
                if(e == aux || (aux.Date.Day.CompareTo(e.Date.Day) == 0 && aux.Local == e.Local && aux.Type == e.Type))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Adiciona evento na lista de eventos
        /// </summary>
        /// <param name="e"></param>
        public static void AddToList(Event e)
        {
            events.Add(e);
        }

        /// <summary>
        /// Realizar um evento
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool DoEvent(Event e)
        {
            e.State = STATE.done;
            return true;
        }

        /// <summary>
        /// Devolve lista nova de eventos num periodo de tempo, novos ou realizados
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="higher"></param>
        /// <returns></returns>
        public static List<Event> EventsBetweenDates(DateTime lower, DateTime higher)
        {
            if (lower.CompareTo(higher) > 0)
            {
                DateTime auxiliarDate = lower;
                lower = higher;
                higher = auxiliarDate;
            }

            List<Event> aux = new List<Event>();

            foreach(Event x in events)
            {
                if (x.State == STATE.recent || x.State == STATE.done
                    && (x.Date.CompareTo(lower) > 0 && x.Date.CompareTo(higher) < 0)) aux.Add(x); 
            }

            return aux;
        }

        /// <summary>
        /// Verifica se event existe
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Event VerifyEventExists(int id)
        {
            foreach(Event x in events)
            {
                if (id == x.Id)
                {
                    return x;
                }
            }

            return null;
        }

        #endregion
    }

    class InfoAux
    {
        public static int InputID()
        {
            Console.Write("Input an ID: ");
            int aux = int.Parse(Console.ReadLine());

            while (aux <= 0)
            {
                Console.Write("Input an ID: ");
                aux = int.Parse(Console.ReadLine());
            }

            return aux;
        }
        public static DateTime InputDate()
        {
            DateTime aux;
            Console.Clear();
            Console.Write("Input Date: ");
            aux = DateTime.Parse(Console.ReadLine());

            while ((aux.CompareTo(DateTime.MaxValue) > 0 || aux.Date.CompareTo(DateTime.MaxValue) < 0)
                && aux.Date.Year.CompareTo(2019) != 0)
            {
                Console.Clear();
                Console.Write("Input Date: ");
                aux = DateTime.Parse(Console.ReadLine());
            }

            return aux;
        }
    }

    class Menu
    {
        #region Methods

        public static void Text()
        {
            int opcao;

            do
            {
                Console.Write("[1] - Create Event\n[2] - Update Event" +
                    "\n[3] - Recent and Done Events in a Period of time\n" +
                    "[4] - Verify if Event exists throught ID\n[0] - Sair\n\nOPTION ===> ");

                opcao = int.Parse(Console.ReadLine());

                while (opcao < 0 && opcao > 4)
                {
                    Console.Clear();
                    Console.Write("[1] - Create Event\n[2] - Update Event" +
                    "\n[3] - Recent and Done Events in a Period of time\n" +
                    "[4] - Verify if Event exists throught ID\n[0] - Sair\n\nOPTION ===> ");

                    opcao = int.Parse(Console.ReadLine());
                }

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Event e = Event.CreateEvent();
                        if (!Events.VerifyEventCanBeCreated(e))
                        {
                            Console.WriteLine("Something went wrong... Create a new Event later");
                        }
                        else
                        {
                            Events.AddToList(e);
                            Console.WriteLine("Event added with sucess!");
                        }
                        Console.WriteLine("\nPressione qualquer botao para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        Event e2 = Events.VerifyEventExists(InfoAux.InputID());
                        if (e2 != null)
                        {
                            Event.UpdateEventState(e2);
                            Console.WriteLine("Event's state changed with sucess!");
                        }
                        else Console.WriteLine("That event doesn't exist");
                        Console.WriteLine("\nPressione qualquer botao para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        List<Event> aux = Events.EventsBetweenDates(InfoAux.InputDate(), InfoAux.InputDate());
                        Events.ShowEvents(aux);
                        Console.WriteLine("\nPressione qualquer botao para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        Event e3 = Events.VerifyEventExists(InfoAux.InputID());
                        if (e3 != null) Console.WriteLine("Event exist!");
                        else Console.WriteLine("Event doesn't exist!");
                        Console.WriteLine("\nPressione qualquer botao para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            } while (opcao != 0);

        }

        #endregion
    }
}
