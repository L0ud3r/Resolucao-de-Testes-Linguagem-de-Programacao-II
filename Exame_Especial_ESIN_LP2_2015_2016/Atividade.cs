using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            a.EstadoInfo = "Atividade nova! Por realizar";
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

        //Atividades de uma area num periodo de tempo()
        //(talvez recorrer a 2 datas input e colocar os que estao entre elas)

        //Verificar se atividade existe e lista-la()

        //Guardar em BinaryFiles()
        #endregion
    }

    /// <summary>
    /// Classe com texto e orientacao pelo programa
    /// </summary>
    class Menu
    {
        #region Methods



        #endregion
    }
}
