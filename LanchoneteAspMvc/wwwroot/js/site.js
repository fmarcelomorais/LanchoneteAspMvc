//google.charts.load('current', { packages: ['corechart'] });
//google.charts.setOnLoadCallback(carregaDados);

//async function carregaDados(dias) {
    
//    const url = `@Url.Action("VendasLanches", "AdminGrafico", new { dias = ${dias} })`;
//    console.log(url)
//    const requisicao = await fetch(url)
//    const dados = await requisicao.json();

//    let informacoesGrafico = [
//        ["Nome", "Quantidade", "Valores(R$)", { role: 'style' }]
//    ]
//    $.each(dados, (i, item) => {
//        informacoesGrafico.push([item.nome, item.quantidade, item.valorTotal, `color: #${i}8F5FF`]);
//    });

//    geraGrafico(informacoesGrafico);

//}

//function geraGrafico(dados) {
    
//    let view = google.visualization.arrayToDataTable(dados);

//    const options = {
//        width: 1000,
//        height: 400,
//        bar: { groupWidth: "95%" },
//        legend: { position: "none" },
//    }
//    const grafico = new google.visualization.BarChart(document.getElementById("grafico-anual"));
//    grafico.draw(view, options)

//}

