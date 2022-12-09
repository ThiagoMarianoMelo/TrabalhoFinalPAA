
using System.Diagnostics;

namespace Exercicio1.AlgoritmoGuloso;

// A crescente informatização dos nossos procedimentos corriqueiros, impulsionada pela infeliz
// pandemia COVID-19 que nos atingiu nos últimos anos, tornou cada vez mais comum que façamos
// compras diversas online, recebendo os produtos diretamente em casa. Consequentemente, houve
// expansão dos serviços e das empresas de logística e de entrega de encomendas.
// Em um grande centro de distribuição de uma destas empresas, rotas são geradas automaticamente,
// em frequência semanal, garantindo que todos os produtos ali alocados cheguem aos seus destinatários
// o quanto antes. No entanto, não há uma política de distribuição das rotas, ocasionando que,
// eventualmente, um caminhão de entrega rode muitos quilômetros e sobrecarregue o motorista,
// enquanto outros caminhões estão parados.
// Sua tarefa, então, é criar um algoritmo que receba um conjunto de rotas (representadas por um
// identificador e seu comprimento em quilômetros) e um número de caminhões disponíveis. A partir
// destes dados, o algoritmo deve distribuir as rotas entre os caminhões de maneira a minimizar a
// diferença de quilometragem total rodada entre eles. 

//Classe que irá representar a Rota de cada caminhão
internal class Rota{
    public Rota(string identificador, int comprimentoRota){
        this.comprimentoRota  = comprimentoRota;
        this.identificador    = identificador;
    }
    public string identificador { get; set; }
    public int    comprimentoRota { get; set; }
}
//Classe que irá representar o caminhão
internal class Caminhao{
    public Caminhao(string identificador){
        this.identificador = identificador;
        this.rotaCaminhao = new List<Rota>();
    }
    public string identificador { get; set; }
    public List<Rota> rotaCaminhao { get; set; }
    public int totalRota { get; set; }
}


internal class Program{
    static void Main(String[] args){
        var listaRotas = new List<Rota>();
        //preenchimento com rotas aleatorias
        listaRotas.Add(new Rota("Rota1", 80));
        listaRotas.Add(new Rota("Rota2", 25));
        listaRotas.Add(new Rota("Rota3", 60));
        listaRotas.Add(new Rota("Rota4", 50));
        listaRotas.Add(new Rota("Rota5", 5));
        listaRotas.Add(new Rota("Rota6", 11));
        listaRotas.Add(new Rota("Rota7", 31));
        listaRotas.Add(new Rota("Rota8", 33));
        listaRotas.Add(new Rota("Rota9", 100));

        var listaCaminhoes = new List<Caminhao>();
        listaCaminhoes.Add(new Caminhao("Caminhao1"));
        listaCaminhoes.Add(new Caminhao("Caminhao2"));
        listaCaminhoes.Add(new Caminhao("Caminhao3"));
        listaCaminhoes.Add(new Caminhao("Caminhao4"));
        listaCaminhoes.Add(new Caminhao("Caminhao5"));
        
        //Comecando a contar tempo de execucao do metodo
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        //Primeiro é necessario ordenar o vetor pelo km de forma descrescente ou crescente;
        var listaRotasOrdenado = listaRotas.OrderByDescending( r => r.comprimentoRota);
        foreach(var rota in listaRotasOrdenado){
            var caminhaoMinRota = getMinRotaCaminhao(listaCaminhoes);
            caminhaoMinRota.rotaCaminhao.Add(rota);
            caminhaoMinRota.totalRota += rota.comprimentoRota;
        }

        stopwatch.Stop();

        foreach (var caminhao in listaCaminhoes){
            Console.WriteLine($"Caminhao: {caminhao.identificador} total Km: {caminhao.totalRota}");
        }

        Console.WriteLine($"Tempo de execucao: {stopwatch.ElapsedMilliseconds}");
        //Media de 1 milisegundo
    }

    //Metodo para receber o caminhao com menor rota
    public static Caminhao? getMinRotaCaminhao(List<Caminhao> caminhoes){
        Caminhao caminhaoMinRota = null;
        var minRota = 9999;
        foreach (var caminhao in caminhoes){
            if(caminhao.totalRota < minRota){
                caminhaoMinRota = caminhao;
                minRota = caminhao.totalRota;
            }
        }
        return caminhaoMinRota;
    }
}



