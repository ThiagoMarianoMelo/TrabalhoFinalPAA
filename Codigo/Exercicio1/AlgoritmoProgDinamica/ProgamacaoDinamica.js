const caminhoes = [1,2,3]
const rotas = [
    {
        id: 1,
        km: 30,
        utilizada: false
    },
    {
        id: 2,
        km: 40,
        utilizada: false
    },
    {
        id: 3,
        km: 20,
        utilizada: false
    },
    {
        id: 4,
        km: 50,
        utilizada: false
    },
    {
        id: 5,
        km: 10,
        utilizada: false
    }
]

const inicio = console.time('time');

const somaKmTotal = rotas.reduce((acc, rotaAtual) => {
    return acc + rotaAtual.km;
},0);

const mediaKm = Math.ceil(somaKmTotal / caminhoes.length);

function criarTabelaInicial() {
    const tabela = [];
    const quantidadeRotas = rotas.length;

    for(let caminhao in caminhoes) {
        tabela.push(Array(quantidadeRotas).fill(0));
    }

    return tabela;
}

const tabela = criarTabelaInicial();

for(let i = 0; i < caminhoes.length; i++) {
    for(let j = 0; j < rotas.length; j++) {
        const valoresUtilizados = [];
        let somaResultadosLinhaAtual = tabela[i].reduce((acc, valorAtual) => {
            if (!valoresUtilizados.includes(valorAtual)) {
                valoresUtilizados.push(valorAtual);
                return acc + valorAtual;
            }
            return acc;
        },0);

        if (!rotas[j].utilizada) {
            const valorASerAdicionado = somaResultadosLinhaAtual + rotas[j].km;

            if (valorASerAdicionado <= mediaKm) {
                tabela[i][j] = valorASerAdicionado;
                rotas[j].utilizada = true;
            }
            else {
                tabela[i][j] = tabela[i][j - 1];
            }
        }
    }
}

for(let caminhao of tabela) {
    const maiorValorArray = Math.max(...caminhao);
    caminhao[rotas.length - 1] = maiorValorArray 
}

while(rotas.some(rota => !rota.utilizada)) {
    let elementoMenorCaminhao = tabela[0];
    let valorMenorCaminhao = tabela[0][rotas.length - 1]; // Primeira linha e ultima posição do vetor
    
    for(let caminhao of tabela) {
        if (caminhao[rotas.length - 1] < valorMenorCaminhao) {
            elementoMenorCaminhao = caminhao;
            valorMenorCaminhao = caminhao[rotas.length - 1]
        }
    }

    const rotaAindaNaoUtilizada = rotas.find(rota => !rota.utilizada);

    if (rotaAindaNaoUtilizada) {
        elementoMenorCaminhao[rotas.length - 1] = valorMenorCaminhao + rotaAindaNaoUtilizada.km;
        rotaAindaNaoUtilizada.utilizada = true
    }
}

function encontrarRotasUtilizadasParaCadaCaminhao() {
    const trajetos = [];

    for(let caminhao of tabela) {
        const rotasCaminhao = [];
        let maiorValorAtual = 0;
        let ultimoValor = 0;

        for(let i=0; i <= caminhao.length; i++) {
            if (maiorValorAtual < caminhao[i] && caminhao[i] !== 0) {
                ultimoValor = maiorValorAtual;
                maiorValorAtual = caminhao[i];

                const rotaComValorEmKM = rotas.find(rota => rota.km + ultimoValor === maiorValorAtual);
                rotasCaminhao.push(rotaComValorEmKM)
            }
        }

        trajetos.push(rotasCaminhao);
    }

    trajetos.forEach((trajeto, i) => {
        const totalKm = trajeto.reduce((acc, rotaAtual) => {
            return acc + rotaAtual.km;
        }, 0)

        const rotasString = trajeto.reduce((acc, rotaAtual) => {
            return acc + rotaAtual.id + '-';
        }, '');

        console.log(`O caminhão ${caminhoes[i]} fará um trajeto pelas rotas ${rotasString}, com um valor total de ${totalKm} KM`);
    })
}

encontrarRotasUtilizadasParaCadaCaminhao();

console.timeEnd('time')