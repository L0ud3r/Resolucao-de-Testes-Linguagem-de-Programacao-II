using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exame_Especial_ESIN_LP2_2015_2016
{
    public enum AREA { desporto, cultura, fotografia, ciencia }
    public enum ESTADO { adiado, cancelado, novo, realizado }
    class Atividade
    {
        #region Attributes
        string nome;
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
            bool aux = Atividades.VerificaExisteAtividade(a);

            if (aux == false) return null;
            return a;
        }
        #endregion
    }

    class Atividades
    {
        //Attributes
        static List<Atividade> atividades;

        //Constructors
        static Atividades()
        {
            atividades = new List<Atividade>();
        }
        //Properties

        //Methods

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

        public static void UpdateState(Atividade a)
        {
            //Alterar estado
        }
    }
}
