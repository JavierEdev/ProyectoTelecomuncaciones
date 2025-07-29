const ctx = document.getElementById('luxChart').getContext('2d');
const lastValueSpan = document.getElementById('lastValue');

const luxData = {
  labels: [],
  datasets: [{
    label: 'Intensidad de Luz (Lux)',
    data: [],
    borderColor: '#3B82F6',
    backgroundColor: 'rgba(59, 130, 246, 0.1)',
    tension: 0.3,
    pointRadius: 2,
    fill: true
  }]
};

const config = {
  type: 'line',
  data: luxData,
  options: {
    responsive: true,
    scales: {
      x: {
        title: { display: true, text: 'Hora' }
      },
      y: {
        title: { display: true, text: 'Lux (lm)' },
        beginAtZero: true
      }
    }
  }
};

const luxChart = new Chart(ctx, config);

function fetchLuxData() {
  fetch('http://[2803:d100:f228:3112:ae2a:dc0d:5c1a:df8f]:5039/api/Lux')
    .then(res => res.json())
    .then(data => {
      const now = new Date().toLocaleTimeString();
      const value = data.lux;

      luxChart.data.labels.push(now);
      luxChart.data.datasets[0].data.push(value);

      if (luxChart.data.labels.length > 30) {
        luxChart.data.labels.shift();
        luxChart.data.datasets[0].data.shift();
      }

      lastValueSpan.textContent = value.toFixed(2);
      luxChart.update();
    })
    .catch(error => {
      console.error('Error al obtener lux:', error);
      lastValueSpan.textContent = 'Error';
    });
}

fetchLuxData();
setInterval(fetchLuxData, 3000);
