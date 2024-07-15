google.charts.load('current', { packages: ['corechart'] });
google.charts.setOnLoadCallback(carregaDados);

async function carregaDados() {
    
    const url1 = `@Url.Action("VendasLanches", "AdminGrafico", new { dias = ${dias} })`;
    const url = `https://localhost:7244/Admin/AdminGrafico/${dias}`
    const requisicao = await fetch(url)
    const dados = await requisicao.json();

    let informacoesGrafico = [
        ["Nome", "Quantidade", "Valores(R$)", { role: 'style' }]
    ]
    $.each(dados, (i, item) => {
        informacoesGrafico.push([item.nome, item.quantidade, item.valorTotal, `color: #${i}8F5FF`]);
    });

    geraGrafico(informacoesGrafico);

}

function geraGrafico(dados) {

    let view = google.visualization.arrayToDataTable(dados);

    const options = {
        width: 1000,
        height: 400,
        bar: { groupWidth: "95%" },
        legend: { position: "none" },
    }
    debugger
    const grafico = new google.visualization.BarChart(document.getElementById("grafico"));

    grafico.draw(view, options)

}

