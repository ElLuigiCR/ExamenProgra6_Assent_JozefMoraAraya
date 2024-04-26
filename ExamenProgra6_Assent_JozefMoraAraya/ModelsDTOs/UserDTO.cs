namespace ExamenProgra6_Assent_JozefMoraAraya.ModelsDTOs
{
    public class UserDTO
    {
        public int IdUsuario { get; set; }

        public string Cedula { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string? Telefono { get; set; }

        public string Contrsennia { get; set; } = null!;

        public int Huelgas { get; set; }

        public string Gmail { get; set; } = null!;

        public string? Trabajo { get; set; }

        public int IdPuesto { get; set; }

        public int CedulaRegional { get; set; }

        public int IdApartamento { get; set; }

        public string Activo { get; set; } = null!;

    }
}
