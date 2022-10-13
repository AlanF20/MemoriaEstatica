using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryE
{
    class ParticionEstatica
    {
        private int _intTamañoMemoria;
        public int TamañoMemoria
        {
            get { return _intTamañoMemoria; }
            set { _intTamañoMemoria = value; }
        }


        private int _intTamañoSO;
        public int TamañoSO
        {
            get { return _intTamañoSO; }
            set { _intTamañoSO = value; }
        }
    }
    class Particion
    {
        //Atributos y propiedades
        private string _strNombre;
        public string Nombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }


        private int _intTamaño;
        public int Tamaño
        {
            get { return _intTamaño; }
            set { _intTamaño = value; }
        }
    }
    class Trabajo
    {
        //Atributos y propiedades
        private string _strNombre;
        public string Nombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }


        private int _intTamaño;
        public int Tamaño
        {
            get { return _intTamaño; }
            set { _intTamaño = value; }
        }


        private string _strSituacion;
        public string Situacion
        {
            get { return _strSituacion; }
            set { _strSituacion = value; }
        }
    }
    class Listas<Tipo>
    {
        //Atributo
        private List<Tipo> LT = new List<Tipo>();

        //Metodos
        public void Insertar(Tipo T)
        {
            LT.Add(T);
        }

        public IEnumerator<Tipo> GetEnumerator()
        {
            foreach (Tipo T in LT)
            {
                yield return T;
            }
        }

        public int Contar()
        {
            int C = 0;
            foreach (Tipo T in LT)
            {
                C = C + 1;
            }
            return (C);
        }
    }
    class Estado
    {
        //Atributos y propiedades
        private string _strNombreP;
        public string NombreP
        {
            get { return _strNombreP; }
            set { _strNombreP = value; }
        }

        private int _intTamañoP;
        public int TamañoP
        {
            get { return _intTamañoP; }
            set { _intTamañoP = value; }
        }


        private string _strNombreT;
        public string NombreT
        {
            get { return _strNombreT; }
            set { _strNombreT = value; }
        }


        private string _strSituacion;
        public string Situacion
        {
            get { return _strSituacion; }
            set { _strSituacion = value; }
        }


        private int _intDesperdicio;
        public int Desperdicio
        {
            get { return _intDesperdicio; }
            set { _intDesperdicio = value; }
        }
    }
}
