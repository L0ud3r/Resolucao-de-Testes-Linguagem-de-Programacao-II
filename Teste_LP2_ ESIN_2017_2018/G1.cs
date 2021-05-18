/**
 * Name: Pedro Vieira Simões
 * Contact: a21140@alunos.ipca.pt
 * College: Instituto Politécnico do Cávado e do Ave
 * Course: Licenciatura em Engenharia de Sistemas Informáticos
 * Chair: Linguagem de Programação II
 * Date: 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1
{
    public enum Tipo { tipo1, tipo2}
    class Evento
    {
        DateTime data;
        Tipo tipoEvento;

        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        public Tipo Tipo
        {
            get { return tipoEvento; }
            set { tipoEvento = value; }
        }

        #region Operators
        public static bool operator ==(Evento p1, Evento p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Evento p1, Evento p2)
        {
            return !(p1 == p2);
        }

        public static bool operator <(Evento p1, Evento p2)
        {
            int aux = p1.Data.CompareTo(p2.Data);
            if (aux > 0) return true;
            if (aux < 0) return false;
            return false;
        }

        public static bool operator >(Evento p1, Evento p2)
        {
            return !(p1 < p2);
        }
            #endregion
    }

    class Eventos
    {
        #region Atributos
        static Evento[] eventos;
        #endregion

        public Eventos()
        {
            eventos = new Evento[10];
        }

        #region Metodos
        /// <summary>
        /// Método que devolve o conjunto de eventos que ocorrem 
        /// num determinado dia e de um determinado tipo
        /// </summary>
        public static Evento[] WhatHappens(DateTime d, Tipo t)
        {
            Evento[] aux = new Evento[10];
            int counter = 0;

            for(int i = 0; i < aux.Length; i++)
            {
                if (eventos[i].Tipo == t && eventos[i].Data.CompareTo(d) == 0) aux[counter] = eventos[i];
                counter++;
            }

            return aux;
        }
        /// <summary>
        /// Método que regista um novo evento. Caso se pretenda registar um evento 
        /// repetido, deve ser gerada a exceção "EventExistException"
        /// </summary>
        public static bool AddEvent(Evento e)
        {
            for(int i = 0; i < eventos.Length; i++)
            {
                if (eventos[i] == e)
                {
                    throw new EventExistException();
                }
            }

            return true;
        }
        #endregion
    }

    class EventExistException : ApplicationException
    {
        public EventExistException() : base("Evento ja existente") { }
        public EventExistException(string s) : base(s) { }
        public EventExistException(string s, Exception e)
        {
            throw new EventExistException(e.Message + " - " + s);
        }
    }

    class Object
    {
        #region Attributes



        #endregion

        #region Constructors

        public Object() { }

        #endregion

        #region Properties



        #endregion

        #region Overrides



        #endregion

        #region Methods



        #endregion
    }
}
