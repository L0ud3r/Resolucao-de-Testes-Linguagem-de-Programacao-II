using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Exame_Especial_ESIN_LP2_2015_2016
{
    /// <summary>
    /// Enums
    /// </summary>
    public enum AREA { desporto, cultura, fotografia, ciencia }
    public enum ESTADO { adiado, cancelado, novo, realizado }

    /// <summary>
    /// Class que caracteriza uma atividade
    /// </summary>
    [Serializable]
    class Atividade
    {
        #region Attributes
        string nome, estadoInfo;
        DateTime data;
        static int IdFollower = 0;
        int duracao, id;
        AREA area;
        ESTADO estado;
        #endregion

        #region Constructors
        public Atividade() { }
        #endregion

        #region Properties
        public string Nome 
        {
            get { return nome; }
            set { nome = value; }
        }
        public string EstadoInfo
        {
            get { return estadoInfo; }
            set { estadoInfo = value; }
        }
        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        public int Duracao
        {
            get { return duracao; }
            set { duracao = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public AREA Area
        {
            get { return area; }
            set { area = value; }
        }
        public ESTADO Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Metodo que cria uma atividade
        /// </summary>
        /// <returns></returns>
        public static Atividade CreateAtividade()
        {
            Atividade a = new Atividade();

            //Ler Nome
            Console.Write("Designacao da atividade: ");
            a.Nome = Console.ReadLine();

            while(a.Nome.Length <= 2)
            {
                Console.Write("Designacao da atividade: ");
                a.Nome = Console.ReadLine();
            }

            //Ler Data
            Console.Write("Data da atividade: ");
            a.Data = DateTime.Parse(Console.ReadLine());

            while ((a.Data.Year < 2021 && a.Data.Year > 2100) && (a.Data.Month < 1 && a.Data.Month > 12)
                && (a.Data.Day < 2021 && a.Data.Day > 2100) && (a.Data.Hour < 00 && a.Data.Year > 23)
                && (a.Data.Minute < 00 && a.Data.Minute > 59) && (a.Data.Second < 00 && a.Data.Second > 59))
            {
                Console.Write("Data da atividade: ");
                a.Data = DateTime.Parse(Console.ReadLine());
            }

            //Ler duracao
            Console.Write("Duracao da atividade: ");
            a.Duracao = int.Parse(Console.ReadLine());

            while (a.Duracao <= 0)
            {
                Console.Write("Duracao da atividade: ");
                a.Duracao = int.Parse(Console.ReadLine());
            }

            //Ler Area
            Console.Write("Area da atividade: \n[1] - Desporto\n[2] - Cultura\n[3] - Fotografia\n[4] - Ciencia\nOPCAO ==> ");
            int opcao = int.Parse(Console.ReadLine());

            while (opcao < 1 || opcao > 4)
            {
                Console.Write("\nArea da atividade: \n[1] - Desporto\n[2] - Cultura\n[3] - Fotografia\n[4] - Ciencia\nOPCAO ==> ");
                opcao = int.Parse(Console.ReadLine());
            }

            if (opcao == 1) a.Area = AREA.desporto;
            else if (opcao == 2) a.Area = AREA.cultura;
            else if (opcao == 3) a.Area = AREA.fotografia;
            else if (opcao == 4) a.Area = AREA.ciencia;

            //Atribuir estado e Id
            a.Estado = ESTADO.novo;
            a.Id = ++IdFollower;
            a.EstadoInfo = "Atividade agendada para o dia " + a.Data;
            bool aux = Atividades.VerificaExisteAtividade(a);

            if (aux == false) return null;
            return a;
        }
        /// <summary>
        /// Alterna estado de uma atividade
        /// </summary>
        /// <param name="a"></param>
        public static void AlternaEstado(Atividade a)
        {
            Console.Write("Alternar estado para: \n[1] - Adiado\n[2] - Cancelado\nOPCAO ==> ");
            int opcao = int.Parse(Console.ReadLine());

            while(opcao != 1 && opcao != 2)
            {
                Console.Write("\nAlternar estado para: \n[1] - Adiado\n[2] - Cancelado\nOPCAO ==> ");
                opcao = int.Parse(Console.ReadLine());
            }

            if(opcao == 1)
            {
                Console.Write("Nova data para adiamento: ");
                DateTime aux = DateTime.Parse(Console.ReadLine());

                while (aux <= a.Data)
                {
                    Console.Write("\nData invalida.\nNova data para adiamento: ");
                    aux = DateTime.Parse(Console.ReadLine());
                }

                a.Data = aux;
                a.EstadoInfo = "Evento adiado para " + a.Data.ToString();
                a.Estado = ESTADO.adiado;
            }
            else
            {
                Console.Write("Motivo de cancelamento: ");
                string aux = Console.ReadLine();

                a.EstadoInfo = "Cancelado devido a " + aux;
                a.Estado = ESTADO.cancelado;
            }
        }
        #endregion
    }

    /// <summary>
    /// Classe que contem uma lista de Atividade
    /// </summary>
    [Serializable]
    class Atividades
    {
        #region Attributes
        static List<Atividade> atividades;
        #endregion

        #region Constructors
        static Atividades()
        {
            atividades = new List<Atividade>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Verifica a existencia da atividade "a" na lista 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool VerificaExisteAtividade(Atividade a)
        {
            if(atividades.Count != 0) 
            { 
                foreach(Atividade aux in atividades)
                {
                    if (a.Equals(aux)) return false;
                }
            }

            return true;
        }
        /// <summary>
        /// Adiciona uma atividade criada na lista
        /// </summary>
        /// <returns></returns>
        public static bool AddNewAtividade()
        {
            Atividade atividade = Atividade.CreateAtividade();
            if (atividade == null) return false;

            atividades.Add(atividade);
            return true;
        }
        /// <summary>
        /// Procura atividade na lista pelo ID e alterna o seu estado
        /// </summary>
        /// <param name="id"></param>
        public static bool UpdateEstadoAtividadeID(int id)
        {
            foreach(Atividade aux in atividades)
            {
                if (aux.Id == id)
                {
                    Atividade.AlternaEstado(aux);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Mostra eventos de uma determinada area num periodo de tempo especifico
        /// </summary>
        /// <param name="area"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public static void MostraAtividadesAreaLimitedTime(AREA area, DateTime min, DateTime max)
        {
            if(min > max)
            {
                DateTime auxDate = min;
                min = max;
                max = auxDate;
            }

            int counter = 0;
            foreach (Atividade aux in atividades)
            {
                if (aux.Area == area && (aux.Data > min && aux.Data < max))
                {
                    Console.WriteLine("Nome: {0}\nData: {1}", aux.Nome, aux.EstadoInfo);
                    counter++;
                }
            }

            if (counter == 0) Console.WriteLine("Nenhuma atividade encontrada");
        }

        /// <summary>
        /// Verifica existencia de uma atividade pelo ID e mostra-o se encontrar
        /// </summary>
        /// <param name="id"></param>
        public static void VerificaExisteAtividadeID(int id)
        {
            int count = 0;

            foreach (Atividade aux in atividades)
            {
                if (aux.Id == id)
                {
                    Console.WriteLine("Nome da atividade: {0}\nEstado: {1}\nDuracao: {2}\nArea: {3}", aux.Nome, aux.EstadoInfo, aux.Duracao, aux.Area.ToString());
                    count++;
                }
            }

            if (count == 0) Console.WriteLine("Nenhuma estufa encontrada");
        }

        /// <summary>
        /// Metodo que salva informacao atual em files
        /// </summary>
        public static void SalvarDados()
        {
            Stream s = File.Open("Teste2.txt", FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter bfw = new BinaryFormatter();
            bfw.Serialize(s, atividades);
        }
        #endregion
    }

    class InfoAux
    {
        #region Methods
        /// <summary>
        /// Retorna ID insirido no stdin
        /// </summary>
        /// <returns></returns>
        public static int GetID()
        {
            Console.Write("Insira um ID: ");
            int aux = int.Parse(Console.ReadLine());

            while(aux <= 0)
            {
                Console.Write("Insira um ID: ");
                aux = int.Parse(Console.ReadLine());
            }

            return aux;
        }
        /// <summary>
        /// Retorna data inserida no stdin
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDate()
        {
            Console.Write("Insira uma data: ");
            DateTime aux = DateTime.Parse(Console.ReadLine());

            while ((aux.Year < 2021 && aux.Year > 2100) && (aux.Month < 1 && aux.Month > 12)
                && (aux.Day < 2021 && aux.Day > 2100) && (aux.Hour < 00 && aux.Year > 23)
                && (aux.Minute < 00 && aux.Minute > 59) && (aux.Second < 00 && aux.Second > 59))
            {
                Console.Write("Insira uma data: ");
                aux = DateTime.Parse(Console.ReadLine());
            }

            return aux;
        }
        /// <summary>
        /// Retorn Area inserida no stdin
        /// </summary>
        /// <returns></returns>
        public static AREA GetArea()
        {
            Console.Write("Area: \n[1] - Desporto\n[2] - Cultura\n[3] - Fotografia\n[4] - Ciencia\nOPCAO ==> ");
            int opcao = int.Parse(Console.ReadLine());

            while (opcao < 1 || opcao > 4)
            {
                Console.Write("\nArea: \n[1] - Desporto\n[2] - Cultura\n[3] - Fotografia\n[4] - Ciencia\nOPCAO ==> ");
                opcao = int.Parse(Console.ReadLine());
            }

            if (opcao == 1) return AREA.desporto;
            else if (opcao == 2) return AREA.cultura;
            else if (opcao == 3) return AREA.fotografia;
            else if (opcao == 4) return AREA.ciencia;
            //Return por defeito
            return AREA.desporto;
        }
        #endregion
    }

    /// <summary>
    /// Classe com texto e orientacao pelo programa
    /// </summary>
    class Menu
    {
        #region Methods

        public static void Texto()
        {
            int opcao;

            do
            {
                Console.Write("[1] - Registar atividade\n[2] - Atualizar Estado" +
                    "\n[3] - Atividades de uma area num periodo de tempo\n[4] - Verificar se atividade existe pelo ID" +
                    "\n[5] - Salvar informacao\n[0] - Sair\n\nOPCAO ===> ");
                opcao = int.Parse(Console.ReadLine());

                while (opcao < 0 && opcao > 5)
                {
                    Console.Clear();
                    Console.Write("[1] - Registar atividade\n[2] - Atualizar Estado" +
                    "\n[3] - Atividades de uma area num periodo de tempo\n[4] - Verificar se atividade existe pelo ID" +
                    "\n[5] - Salvar informacao\n[0] - Sair\n\nOPCAO ===> ");
                    opcao = int.Parse(Console.ReadLine());
                }

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        bool method1 = Atividades.AddNewAtividade();
                        if (method1) Console.WriteLine("\n\nEvento criado com sucesso!");
                        else Console.WriteLine("\n\nErro ao criar evento!");
                        Console.WriteLine("\nPressione qualquer botao para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        bool method2 = Atividades.UpdateEstadoAtividadeID(InfoAux.GetID());
                        if (method2) Console.WriteLine("\n\nEvento alterado com sucesso!");
                        else Console.WriteLine("\n\nErro ao alterar estado do evento!");
                        Console.WriteLine("\nPressione qualquer botao para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        Atividades.MostraAtividadesAreaLimitedTime(InfoAux.GetArea(), InfoAux.GetDate(), InfoAux.GetDate());
                        Console.WriteLine("\nPressione qualquer botao para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        Atividades.VerificaExisteAtividadeID(InfoAux.GetID());
                        Console.WriteLine("\nPressione qualquer botao para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        Atividades.SalvarDados();
                        Atividades.SalvarDados();
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
