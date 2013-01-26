using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnatidaeHaxball.Models
{
    public class JogadorModels
    {
        //private static readonly IDictionary<string, string> _posicoes = new Dictionary<string, string>()
        //{
        //    {"GK", "Guarda-Redes"},
        //    {"MD", "Médio"},
        //    {"PL", "Avançado"},
        //    {"PV", "Poli-Valente"},
        //    {"MH", "Membro Honorário"}
        //};

        public class Position
        {
            public string Id { get; set; }
            public string Name { get; set; }

        }

        List<Position> _posicoes = new List<Position>()
        {
            new Position() { Id = "GK", Name = "Guarda-Redes"},
            new Position() { Id = "MD", Name = "Médio"},
            new Position() { Id = "PL", Name = "Avançado"},
            new Position() { Id = "PV", Name = "Poli-Valente"},
            new Position() { Id = "MH", Name = "Membro Honorário"}
        };



        public Jogador Jogador { get; set; }
        public List<Position> Posicoes { get { return _posicoes; } }

    }
}