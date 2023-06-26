namespace AgendaPark_Backend.dtos
{
    public class dtoLaboratorio
    {
        public string nome { get; set; }

        public string sigla { get; set; }

        public float m2 { get; set; }

        public int capacidade { get; set; }
        
        public int predioid { get; set; }

        public string localiza_dentro_Predio { get; set; }
    }
}