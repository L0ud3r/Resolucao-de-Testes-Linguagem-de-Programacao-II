using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Teste_LP2N_2018_2019
{
    class Lib1
    {
        public class BaseClass
        {
            public int x;
            double y;
            public BaseClass(int x, int y) 
            {
                this.x = x;
                this.y = (double)y;
            }

            public static bool operator ==(BaseClass obj1, BaseClass obj2)
            {
                return (obj1.x == obj2.x && obj1.y == obj2.y);
            }

            public static bool operator !=(BaseClass obj1, BaseClass obj2)
            {
                return !(obj1.x == obj2.x && obj1.y == obj2.y);
            }

            public override bool Equals(object obj)
            {
                return x == ((BaseClass)obj).x;
            }
            public override string ToString()
            {
                return string.Format("x = {0} - y={1}", x, y);
            }
        }
        abstract class Essential : BaseClass, IEssencial
        {

            public Essential(int x, int y) : base(x, y) { }
            public abstract BaseClass MaxValue(ArrayList y);
            /// <summary>
            /// Devolve o conjunto de valores da Lista "values" que são superiores ao 
            /// valor do parâmetro “x”. Devolve ainda a quantidade de valores que não
            /// verifica essa condição.
            /// </summary>
            public int WhatValues(int x, List<int> values, out List<int> aux) 
            {
                int counter = 0;
                aux = new List<int>();

                foreach (int i in values)
                {
                    if (i > x)
                    {
                        aux.Add(x);
                    }
                    else counter++;
                }

                return counter;
            }
            public override bool Equals(object obj)
            {
                return x.Equals((BaseClass)obj);
            }
        }
        interface IEssencial
        {
            /// <summary>
            /// Devolve o maior valor de um arraylist de objectos do tipo BaseClass
            /// </summary>
            BaseClass MaxValue(ArrayList y);
            /// <summary>
            /// Verifica se os dois valores dos parametros são iguais
            /// </summary>
            bool Equals(Object x, Object y);
        }

        class ExercicioD : Essential
        {
            /// <summary>
            /// Devolve o maior valor de um arraylist de objectos do tipo BaseClass
            /// </summary>
            override public BaseClass MaxValue(ArrayList y)
            {
                BaseClass maior = new BaseClass(int.MinValue, int.MinValue);

                foreach (BaseClass aux in y)
                {
                    if(maior.x < aux.x) maior.x = aux.x;
                }

                return maior;
            }

            public new bool Equals(Object x, Object y)
            {
                if ((BaseClass)x == (BaseClass)y) return true;

                return false;
            }
        }
    }
}
