class Usuario{
    public int Matricula{get;}
    public string Nome{get;}
    public string Email{get;}
    
    public Usuario(int matricula, string nome, string email){
        if(matricula < 0){
            throw new ArgumentException("Matricula inválida!");
        }
        if(string.IsNullOrWhiteSpace(nome)){
            throw new ArgumentException("Nome obrigatório!");
        }
        if(string.IsNullOrWhiteSpace(email)){
            throw new ArgumentException("Email obrigatório!");
        }
        Matricula = matricula;
        Nome = nome;
        Email = email;
    }
}