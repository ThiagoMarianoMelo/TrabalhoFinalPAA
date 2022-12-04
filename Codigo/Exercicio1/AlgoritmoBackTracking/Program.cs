
using System.Diagnostics;

namespace Exercicio1.AlgoritmoBackTracking;

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
        listaRotas.Add(new Rota("Rota1", 80)); // 
        listaRotas.Add(new Rota("Rota2", 25)); //
        listaRotas.Add(new Rota("Rota3", 60)); //
        listaRotas.Add(new Rota("Rota4", 50)); //
        listaRotas.Add(new Rota("Rota5", 5)); //
        listaRotas.Add(new Rota("Rota6", 11)); //
        listaRotas.Add(new Rota("Rota7", 31)); // 
        listaRotas.Add(new Rota("Rota8", 33)); //
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

        //Melhor solucao atual
        int distanciaEntreMaxAndMinRotas = 395; //Seria colocar todas rotas no primeiro caminhão assim como no guloso

        //O dado de comparacao sera olhar a diferenca entre a maior rota em km e a menor, como queremos minimzar a diferenca entre
        //as rotas basta buscar onde essa diferenca for a minima possivel

        var listaRotasOrdenado = listaRotas;
        int pontoDeInicio = 0;
        int i = pontoDeInicio; //marcador para ajudar no resete da lista

        foreach (var rotas in listaRotasOrdenado){
            for (int j = 0; j < listaCaminhoes.Count(); j++)
            {   var caminhao = listaCaminhoes.ElementAt(j);
                var novoTotal = caminhao.totalRota + rotas.comprimentoRota;
                if(novoTotal - findMinRota(listaCaminhoes) <= distanciaEntreMaxAndMinRotas || caminhao.totalRota == 0 ){
                    caminhao.rotaCaminhao.Add(rotas);
                    caminhao.totalRota += rotas.comprimentoRota;
                    distanciaEntreMaxAndMinRotas = findMaxRota(listaCaminhoes) - findMinRota(listaCaminhoes);
                    j = listaCaminhoes.Count();
                }
                 i++;
                if(i == listaCaminhoes.Count()){
                pontoDeInicio ++;
                i = 0;
                }
            }
        }

        Console.WriteLine($"Tempo de execucao: {stopwatch.ElapsedMilliseconds}");
        //Media de 1 milisegundo

        foreach (var caminhao in listaCaminhoes)
        {
            Console.WriteLine($"{caminhao.identificador}: {caminhao.totalRota}");
        }
    }


    private static int findMinRota(List<Caminhao> caminhoes){
        int minRoute = 999999;
        foreach (var caminhao in caminhoes)
        {
            if(caminhao.totalRota < minRoute) minRoute = caminhao.totalRota;
        }

        return minRoute;
    }

        private static int findMaxRota(List<Caminhao> caminhoes){
        int maxRoute = 0;
        foreach (var caminhao in caminhoes)
        {
            if(caminhao.totalRota > maxRoute) maxRoute = caminhao.totalRota;
        }

        return maxRoute;
    }
}



