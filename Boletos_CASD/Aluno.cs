namespace Boletos_CASD
{
	class Aluno
	{
		public int id;
		public string nome;
		public string email;
		public string boleto;

		public Aluno ()
		{
			id = 0;
			nome = "Sem nome";
			email = "Sem email cadastrado";
			boleto = "Sem diretório do boleto";
		}

		public Aluno(int _id, string _nome, string _email, string _boleto)
		{
			id = _id;
			nome = _nome;
			email = _email;
			boleto = _boleto;
		}

		public Aluno(string _id, string _nome, string _email, string _boleto)
		{
			id = int.Parse(_id);
			nome = _nome;
			email = _email;
			boleto = _boleto;
		}
	}
}
