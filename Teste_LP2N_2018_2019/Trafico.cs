using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public enum LOCAL { rioTinto, amadora, santaTecla, pertoDoUrban, angola }
public enum CADASTRO { novo, recorrente }

namespace Teste_LP2N_2018_2019
{
    class Traficante
    {
        #region Attributes
        static int trafID = 0;
        string nome;
        LOCAL origem;
        int idade;
        CADASTRO cadastro;
        #endregion

        #region Properties
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public LOCAL Origem
        {
            get { return origem; }
            set { origem = value; }
        }
        public int Idade
        {
            get { return idade; }
            set { idade = value; }
        }
        public int TrafID
        {
            get { return trafID; }
            set { trafID = value; }
        }
        public CADASTRO Cadastro
        {
            get { return cadastro; }
            set { cadastro = value; }
        }
        #endregion

        #region Methods

        public static Traficante CreateTraficante()
        {
            Traficante t = new Traficante();

            //nome
            Console.WriteLine("Nome do traficante: ");
            t.Nome = Console.ReadLine();

            //idade
            Console.WriteLine("Idade do traficante: ");
            t.Idade = int.Parse(Console.ReadLine());

            while(t.Idade <= 0)
            {
                Console.WriteLine("Idade do traficante: ");
                t.Idade = int.Parse(Console.ReadLine());
            }

            //origem
            Console.WriteLine("Origem do traficante:\n[1] - Rio Tinto\n[2] - Amadora" +
                "\n[3] - Santa Tecla\n[4] - Perto do Urban\n[5] - Angola");
            int aux = int.Parse(Console.ReadLine());

            while (aux > 5 || aux < 1)
            {
                Console.WriteLine("Origem do traficante:\n[1] - Rio Tinto\n[2] - Amadora" +
                "\n[3] - Santa Tecla\n[4] - Perto do Urban\n[5] - Angola");
                aux = int.Parse(Console.ReadLine());
            }

            if (aux == 1) t.Origem = LOCAL.rioTinto;
            else if (aux == 2) t.Origem = LOCAL.amadora;
            else if (aux == 3) t.Origem = LOCAL.santaTecla;
            else if (aux == 4) t.Origem = LOCAL.pertoDoUrban;
            else if (aux == 5) t.Origem = LOCAL.angola;

            t.Cadastro = CADASTRO.novo;

            if (!Traficantes.VerificaTraficanteExiste(t)) return null;
            t.TrafID = ++trafID;
            return t;
        }

        public static void MostraTraficante(Traficante t)
        {
            Console.WriteLine("Nome: {0}\nIdade: {1}\nOrigem: {2}\nCadastro: {3}\n", t.Nome, t.Idade, t.Origem, t.Cadastro);
        }

        #endregion
    }
    class Traficantes
    {
        #region Attributes
        static List<Traficante> traficantes;
        #endregion

        #region Constructors
        static Traficantes()
        {
            traficantes = new List<Traficante>();
        }
        #endregion

        #region Methods

        public static bool VerificaIDTraficante(int id)
        {
            foreach (Traficante aux in traficantes)
            {
                if (aux.TrafID == id) return true;
            }

            return false;
        }
        public static Traficante DevolveTraficanteID(int id)
        {
            foreach (Traficante aux in traficantes)
            {
                if (aux.TrafID == id) return aux;
            }

            return null;
        }
        public static void MostraListaTraficantesOrigem(LOCAL origem)
        {
            int counter = 0;
            foreach(Traficante t in traficantes)
            {
                if (t.Origem == origem)
                {
                    counter++;
                    Traficante.MostraTraficante(t);
                }
            }

            if (counter == 0) Console.WriteLine("Nenhum resultado encontrado");
        }
        public static bool VerificaTraficanteExiste(Traficante t)
        {
            foreach (Traficante aux in traficantes)
            {
                if (aux.Equals(t)) return false;
            }

            return true;
        }
        public static bool NewTraffic()
        {
            Traficante novoTraficante = Traficante.CreateTraficante();
            if (novoTraficante == null) return false;
            traficantes.Add(novoTraficante);

            return true;
        }
        #endregion
    }
    class Apreensao
    {
        #region Attributes
        Traficante traficante;
        string codigoDroga;
        LOCAL local;
        int quantidade;
        DateTime dataApreensao;
        #endregion

        #region Properties
        public string CodigoDroga
        {
            get { return codigoDroga; }
            set { codigoDroga = value; }
        }
        public LOCAL Local
        {
            get { return local; }
            set { local = value; }
        }
        public int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }
        public DateTime DataApreensao
        {
            get { return dataApreensao; }
            set { dataApreensao = value; }
        }
        public Traficante Traficante
        {
            get { return traficante; }
            set { traficante = value; }
        }
        #endregion

        #region Methods
        public static void MostraApreensao(Apreensao a)
        {
            Console.WriteLine("Tranficante: {0}\nCodigo da droga: {1}\nLocal: {2}\nQuantidade: {3}kg" +
                "\nData da apreensao: {4}", a.Traficante.Nome, a.CodigoDroga, a.Local, a.Quantidade, a.DataApreensao.Day + "/" + a.DataApreensao.Month + "/" + a.DataApreensao.Year + "/");
        }
        public static Apreensao CreateApreensao()
        {
            Apreensao a = new Apreensao();

            //traficante
            int id = InfoAux.GetID();
            a.Traficante = Traficantes.DevolveTraficanteID(id);

            //nome
            Console.WriteLine("Codigo da droga apreendida: ");
            a.CodigoDroga = Console.ReadLine();

            //data
            Console.WriteLine("Data da apreensao: ");
            a.DataApreensao = DateTime.Parse(Console.ReadLine());

            //local
            Console.WriteLine("Local de apreensao:\n[1] - Rio Tinto\n[2] - Amadora\n[3] - Santa Tecla" +
                "\n[4] - Perto do Urban\n[5] - Angola");
            int aux = int.Parse(Console.ReadLine());

            while(aux > 5 || aux < 1)
            {
                Console.WriteLine("Local de apreensao:\n[1] - Rio Tinto\n[2] - Amadora\n[3] - Santa Tecla" +
                "\n[4] - Perto do Urban\n[5] - Angola");
                aux = int.Parse(Console.ReadLine());
            }

            if (aux == 1) a.Local = LOCAL.rioTinto;
            else if (aux == 2) a.Local = LOCAL.amadora;
            else if (aux == 3) a.Local = LOCAL.santaTecla;
            else if (aux == 4) a.Local = LOCAL.pertoDoUrban;
            else if (aux == 5) a.Local = LOCAL.angola;

            //quantidade
            try
            {
                Console.WriteLine("Quantidade de " + a.CodigoDroga + " apreendida: ");
                a.Quantidade = int.Parse(Console.ReadLine());
            }
            catch(TrafficInvalidException e)
            {
                Console.WriteLine("Erro - " + e.Message);
            }

            if (!Apreensoes.VerificaApreensaoExiste(a)) return null;

            return a;
        }

        #endregion
    }
    class Apreensoes
    {
        #region Attributes
        static List<Apreensao> apreensoes;
        #endregion

        #region Constructors
        static Apreensoes()
        {
            apreensoes = new List<Apreensao>();
        }
        #endregion

        #region Methods
        public static int QuantidadeDrogaAssociadaTraficante(Traficante t)
        {
            int counter = 0;

            foreach(Apreensao a in apreensoes)
            {
                if (a.Traficante.Equals(t)) counter += a.Quantidade;
            }

            return counter;
        }
        public static bool VerificaApreensaoExiste(Apreensao a)
        {
            foreach(Apreensao aux in apreensoes)
            {
                if (aux.Equals(a)) return false;
            }

            return true;
        }
        public static bool NewTraffic()
        {
            Apreensao novaApreensao = Apreensao.CreateApreensao();
            if (novaApreensao == null) return false;
            apreensoes.Add(novaApreensao);

            return true;
        }
        public static void ApreensoesLimitedTime(DateTime min, DateTime max)
        {
            if(min.CompareTo(max) > 0)
            {
                DateTime auxiliar = min;
                min = max;
                max = auxiliar;
            }

            foreach(Apreensao a in apreensoes)
            {
                if (a.DataApreensao.CompareTo(min) > 0 && a.DataApreensao.CompareTo(min) < 0) 
                    Apreensao.MostraApreensao(a);
            }
        }
        public static List<Apreensao> GetTraffics(int quantidade, DateTime min, DateTime max)
        {
            List<Apreensao> auxList = new List<Apreensao>();

            if (min.CompareTo(max) > 0)
            {
                DateTime auxiliar = min;
                min = max;
                max = auxiliar;
            }

            foreach (Apreensao a in apreensoes)
            {
                if (a.Quantidade > quantidade && 
                    (a.DataApreensao.CompareTo(min) > 0 && a.DataApreensao.CompareTo(min) < 0)) 
                    auxList.Add(a);
            }

            return auxList;
        }
        public static List<Apreensao> SortTraffics(LOCAL local)
        {
            List<Apreensao> auxList = new List<Apreensao>();

            foreach (Apreensao a in apreensoes)
            {
                if (a.Local == local) auxList.Add(a);
            }

            auxList.Sort(new MyComparer());

            return auxList;
        }
        public static void LoadInfo()
        {
            Stream s = File.Open("Teste3_Apreensoes.bin", FileMode.Open, FileAccess.Read);
            BinaryFormatter b = new BinaryFormatter();
            apreensoes = (List<Apreensao>)b.Deserialize(s);

            s.Close();
        }
        public static void TopTen(LOCAL local)
        {
            List<Apreensao> auxList = new List<Apreensao>();

            foreach(Apreensao aux in apreensoes)
            {
                if (aux.Local == local) auxList.Add(aux);
            }

            auxList.Sort(new MyComparer());

            Console.WriteLine("TOP TEN Traficantes:\n");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i.ToString() + " - " + auxList[i].Traficante.Nome);
            }
        }

        #endregion
    }
    class InfoAux
    {
        public static int GetID()
        {
            Console.WriteLine("ID do traficante: ");
            int id = int.Parse(Console.ReadLine());
            bool aux = Traficantes.VerificaIDTraficante(id);

            while (aux == false)
            {
                Console.WriteLine("ID do traficante: ");
                id = int.Parse(Console.ReadLine());
                aux = Traficantes.VerificaIDTraficante(id);
            }

            return id;
        }
        public static DateTime GetDate()
        {
            Console.WriteLine("Data: ");
            DateTime aux = DateTime.Parse(Console.ReadLine());

            return aux;
        }
    }
    class Menu
    {
        //MenuTexto() To do... 
    }
    class MyComparer : IComparer<Apreensao>
    {
        public MyComparer() { }

        public int Compare(Apreensao x, Apreensao y)
        {
            return (x.Quantidade > y.Quantidade ? 1 : (x.Quantidade == y.Quantidade ? 0 : -1));
        }
    }
    class TrafficInvalidException : ApplicationException
    {
        public TrafficInvalidException() : base ("Quantidade de droga invalida") { }
        public TrafficInvalidException(string s) : base(s) { }

        public TrafficInvalidException(string s, Exception e) 
        {
            throw new TrafficInvalidException(e.Message + "-" + s); 
        }
    }
}
