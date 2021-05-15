using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public enum LOCAL { rioTinto, amadora, santaTecla, pertoDoUrban, angola }
public enum CADASTRO { novo, recorrente }

namespace Teste_LP2N_2018_2019
{
    class Traficante
    {
        #region Attributes
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
        public CADASTRO Cadastro
        {
            get { return cadastro; }
            set { cadastro = value; }
        }
        #endregion

        #region Methods


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
        #endregion
    }
    class Apreensao
    {
        #region Attributes
        Traficante tranficante;
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
        #endregion

        #region Methods

        public static Apreensao CreateApreensao()
        {
            Apreensao a = new Apreensao();
            
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
            Console.WriteLine("Quantidade de " + a.CodigoDroga + " apreendida: ");
            a.Quantidade = int.Parse(Console.ReadLine());

            if (a.Quantidade <= 0) ;//throw exception

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
        public static void NewTraffic()
        {
            Apreensao novaApreensao = Apreensao.CreateApreensao();
            apreensoes.Add(novaApreensao);
        }
        #endregion
    }
    class InfoAux
    {

    }
    class Menu
    {

    }
    
}
