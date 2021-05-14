/**
 * Name: Pedro Vieira Simões
 * Contact: a21140@alunos.ipca.pt
 * College: Instituto Politécnico do Cávado e do Ave
 * Course: Licenciatura em Engenharia de Sistemas Informáticos
 * Chair: Linguagem de Programação II
 * Date: 14/05/2021 13:06
 * Brief: Aplicacao capaz de gerir varias estufas
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Estufa
{
    public enum REGIAO { Norte, Centro, Sul };
    public enum TIPO { tipo1, tipo2, tipo3 };
    /// <summary>
    /// Estufa description
    /// </summary>
    [Serializable]
    class Estufa
    {
        #region Attributes

        static int contadorEstufas = 0;
        int capacidade, id;
        TIPO tipo;
        REGIAO regiao;

        #endregion

        #region Constructors
        public Estufa() { }

        #endregion

        #region Properties

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int Capacidade
        {
            get { return capacidade; }
            set { capacidade = value; }
        }
        public REGIAO Regiao
        {
            get { return regiao; }
            set { regiao = value; }
        }
        public TIPO Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        #endregion

        #region Overrides

        public static bool operator ==(Estufa e1, Estufa e2)
        {
            return (e1.Id == e2.Id && e1.Regiao == e2.Regiao && e1.Tipo == e2.Tipo && e1.Capacidade == e2.Capacidade);
        }

        public static bool operator !=(Estufa e1, Estufa e2)
        {
            return !(e1.Id == e2.Id && e1.Regiao == e2.Regiao && e1.Tipo == e2.Tipo && e1.Capacidade == e2.Capacidade);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Metodo que cria uma lista e usa a verificacao da existencia do mesmo na lista
        /// </summary>
        /// <returns></returns>
        public static Estufa CreateEstufa()
        {
            Estufa e = new Estufa();

            Console.WriteLine("ID da estufa: {0}", ++contadorEstufas);
            e.Id = contadorEstufas;

            Console.Write("Capacidade de producao: ");
            int aux = int.Parse(Console.ReadLine());
            //while confirma
            e.Capacidade = aux;

            e.Regiao = InfoAux.ObterRegiao();

            e.Tipo = InfoAux.ObterTipo();

            bool aux2 = Estufas.VerificaExisteEstufa(e);

            if (aux2 == false) return null;
            return e;
        }

        /// <summary>
        /// Funcao que mostra informacao de uma estufa especifica
        /// </summary>
        /// <param name="e"></param>
        public static void MostraEstufa(Estufa e)
        {
            Console.WriteLine("ID: " + e.Id);
            Console.Write("Regiao: ");
            if (e.Regiao == REGIAO.Norte) Console.WriteLine("Norte");
            else if (e.Regiao == REGIAO.Centro) Console.WriteLine("Centro");
            else if (e.Regiao == REGIAO.Sul) Console.WriteLine("Sul");
            Console.Write("Tipo: ");
            if (e.Tipo == TIPO.tipo1) Console.WriteLine("Tipo 1");
            else if (e.Tipo == TIPO.tipo2) Console.WriteLine("Tipo 2");
            else if (e.Tipo == TIPO.tipo3) Console.WriteLine("Tipo 3");
            Console.WriteLine("Capacidade de producao: " + e.Capacidade + " Kg/ano");
        }

        #endregion
    }

    /// <summary>
    /// Class que gere uma lista de estufas
    /// </summary>
    [Serializable]
    class Estufas
    {
        #region Attributes
        static List<Estufa> estufas;
        #endregion

        //Review
        #region Constructors
        static Estufas()
        {
            estufas = new List<Estufa>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Metodo que verifica a existencia de uma certa estufa na lista
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool VerificaExisteEstufa(Estufa e)
        {
            foreach(Estufa aux in estufas)
            {
                if (e == aux) return false;
            }

            return true;
        }

        /// <summary>
        /// Metodo que verifica a existencia de um certo ID na lista
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool VerificaID(int id)
        {
            foreach (Estufa aux in estufas)
            {
                if (aux.Id == id) return true;
            }

            return false;
        }

        /// <summary>
        /// Metodo que verifica a existencia de uma certa estufa na lista
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static void VerificaExisteEstufaID(int id)
        {
            foreach (Estufa aux in estufas)
            {
                if (id == aux.Id) Console.WriteLine("ID: {0}\nRegiao: {1}\nTipo: {2}\nCapacidade: {3}\n", aux.Id, aux.Regiao, aux.Tipo, aux.Capacidade);
            }
        }

        /// <summary>
        /// Metodo que adiciona uma estufa à lista
        /// </summary>
        public static void AddNewEstufa()
        {
            Estufa estufa = Estufa.CreateEstufa();
            estufas.Add(estufa);
        }

        /// <summary>
        /// Verifica estufas com capacidade acime de capacidadeProducao do tipo tipo
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="capacidadeProducao"></param>
        public static void VerificarEstufasTipoProducao(TIPO tipo, int capacidadeProducao)
        {
            foreach (Estufa e in estufas)
            {
                if (e.Tipo == tipo && capacidadeProducao < e.Capacidade)
                    Estufa.MostraEstufa(e);
            }
        }
        //To do
        public static void EstufasRegiaoOrderCapacidade(REGIAO regiao)
        {
            List<Estufa> aux = new List<Estufa>();

            foreach(Estufa e in estufas)
            {
                if (e.Regiao == regiao) aux.Add(e);
            }

            aux.Sort(new MyComparer());

            foreach (Estufa e in aux)
            {
                if (e.Regiao == regiao) Console.WriteLine("ID: {0}\nRegiao: {1}\nTipo: {2}\nCapacidade: {3}\n", e.Id, e.Regiao, e.Tipo, e.Capacidade);
            }
        }

        /// <summary>
        /// Metodo que salva informacao atual em files
        /// </summary>
        public static void SalvarDados()
        {
            Stream s = File.Open("file.bin", FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter bfw = new BinaryFormatter();
            bfw.Serialize(s, estufas);
        }
        #endregion
    }

    class InfoAux
    {
        #region Methods
        /// <summary>
        /// Que le a regiao do input do utilizador e retorna-o
        /// </summary>
        /// <returns></returns>
        public static REGIAO ObterRegiao()
        {
            Console.Write("Insira a regiao da estufa:\n[1] Norte   [2] Centro   [3] Sul\nOPCAO ===> ");
            int aux = int.Parse(Console.ReadLine());
           
            while(aux < 1 && aux > 3)
            {
                Console.Write("Insira a regiao da estufa:\n[1] Norte   [2] Centro   [3] Sul\nOPCAO ===> ");
                aux = int.Parse(Console.ReadLine());
            }

            if (aux == 1) return REGIAO.Norte;
            else if (aux == 2) return REGIAO.Centro;
            else if (aux == 3) return REGIAO.Sul;

            return REGIAO.Norte;
        }

        /// <summary>
        /// Que le a tipo do input do utilizador e retorna-o
        /// </summary>
        /// <returns></returns>
        public static TIPO ObterTipo()
        {
            Console.Write("Insira o tipo de estufa:\n[1] Tipo 1   [2] Tipo 2   [3] Tipo 3\nOPCAO ===> ");
            int aux = int.Parse(Console.ReadLine());

            while (aux < 1 && aux > 3)
            {
                Console.Write("Insira o tipo de estufa:\n[1] Tipo 1   [2] Tipo 2   [3] Tipo 3\nOPCAO ===> ");
                aux = int.Parse(Console.ReadLine());
            }
            if (aux == 1) return TIPO.tipo1;
            else if (aux == 2) return TIPO.tipo2;
            else if (aux == 3) return TIPO.tipo3;

            return TIPO.tipo1;
        }

        /// <summary>
        /// Que le a capacidade do input do utilizador e retorna-o
        /// </summary>
        /// <returns></returns>
        public static int ObterCapacidade()
        {
            Console.Write("Insira uma capacidade: ");
            int aux = int.Parse(Console.ReadLine());

            while (aux <= 0)
            {
                Console.Write("Insira uma capacidade: ");
                aux = int.Parse(Console.ReadLine());
            }

            return aux;
        }

        /// <summary>
        /// Que le a capacidade do input do utilizador e retorna-o
        /// </summary>
        /// <returns></returns>
        public static int ObterID()
        {
            Console.Write("Insira um ID: ");
            int aux = int.Parse(Console.ReadLine());
            bool aux2 = Estufas.VerificaID(aux);

            while (aux <= 0 && aux2 == false)
            {
                Console.Write("Insira um ID: ");
                aux = int.Parse(Console.ReadLine());
                aux2 = Estufas.VerificaID(aux);
            }

            return aux;
        }
        #endregion
    }

    class MenuPrincipal
    {
        #region Methods
        public static void Texto()
        {
            int opcao;

            do
            {
                Console.Write("[1] - Registar estufa\n[2] - Verificar estufas de determinado tipo que produzem acima de determinada capacidade" +
                    "\n[3] - Estufas de uma regiao ordenadas por capacidade\n[4] - Verificar se estufa existe\n[5] - Salvar informacao\n[0] - Sair\n\nOPCAO ===> ");
                opcao = int.Parse(Console.ReadLine());

                while (opcao < 0 && opcao > 5)
                {
                    Console.Clear();
                    Console.Write("[1] - Registar estufa\n[2] - Verificar estufas de determinado tipo que produzem acima de determinada capacidade" +
                    "\n[3] - Estufas de uma regiao ordenadas por capacidade\n[4] - Verificar se estufa existe\n[5] - Salvar informacao\n[0] - Sair\n\nOPCAO ===> ");
                    opcao = int.Parse(Console.ReadLine());
                }

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Estufas.AddNewEstufa();
                        Console.WriteLine("\nPressione qualquer botao para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        Estufas.VerificarEstufasTipoProducao(InfoAux.ObterTipo(), InfoAux.ObterCapacidade());
                        Console.WriteLine("\nPressione qualquer botao para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        Estufas.EstufasRegiaoOrderCapacidade(InfoAux.ObterRegiao());
                        Console.WriteLine("\nPressione qualquer botao para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        Estufas.VerificaExisteEstufaID(InfoAux.ObterID());
                        Console.WriteLine("\nPressione qualquer botao para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        Estufas.SalvarDados();
                        Console.WriteLine("\nPressione qualquer botao para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            } while (opcao != 0);

        }
        #endregion
    }

    class MyComparer : IComparer<Estufa>
    {
        public MyComparer() { }
        public int Compare(Estufa x, Estufa y)
        {
             return (x.Capacidade > y.Capacidade ? 1 : (x.Capacidade == y.Capacidade ? 0 : -1));
        }
    }
}
