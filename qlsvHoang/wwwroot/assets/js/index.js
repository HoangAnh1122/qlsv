$(function () {
  "use strict";


  // chart 1

  var options = {
    series: [{
      name: "Net Sales",
      data: [4, 10, 25, 12, 25, 18, 40, 22, 7]
    }],
    chart: {
      //width:150,
      height: 105,
      type: 'area',
      sparkline: {
        enabled: !0
      },
      zoom: {
        enabled: false
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      width: 1.7,
      curve: 'smooth'
    },
    fill: {
      type: 'gradient',
      gradient: {
        shade: 'dark',
        gradientToColors: ['#02c27a'],
        shadeIntensity: 1,
        type: 'vertical',
        opacityFrom: 0.5,
        opacityTo: 0.0,
        //stops: [0, 100, 100, 100]
      },
    },

    colors: ["#02c27a"],
    tooltip: {
      theme: "dark",
      fixed: {
        enabled: !1
      },
      x: {
        show: !1
      },
      y: {
        title: {
          formatter: function (e) {
            return ""
          }
        }
      },
      marker: {
        show: !1
      }
    },
    xaxis: {
      categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep'],
    }
  };

  var chart = new ApexCharts(document.querySelector("#chart1"), options);
    chart.render();
    // chart 1

  var options = {
    series: [{
      name: "Net Sales",
      data: [4, 10, 25, 12, 25, 18, 40, 22, 7]
    }],
    chart: {
      //width:150,
      height: 105,
      type: 'area',
      sparkline: {
        enabled: !0
      },
      zoom: {
        enabled: false
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      width: 1.7,
      curve: 'smooth'
    },
    fill: {
      type: 'gradient',
      gradient: {
        shade: 'dark',
        gradientToColors: ['#02c27a'],
        shadeIntensity: 1,
        type: 'vertical',
        opacityFrom: 0.5,
        opacityTo: 0.0,
        //stops: [0, 100, 100, 100]
      },
    },

    colors: ["#02c27a"],
    tooltip: {
      theme: "dark",
      fixed: {
        enabled: !1
      },
      x: {
        show: !1
      },
      y: {
        title: {
          formatter: function (e) {
            return ""
          }
        }
      },
      marker: {
        show: !1
      }
    },
    xaxis: {
      categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep'],
    }
  };

  var chart = new ApexCharts(document.querySelector("#chart2"), options);
  chart.render();
      // chart 1

  var options = {
    series: [{
      name: "Net Sales",
      data: [4, 10, 25, 12, 25, 18, 40, 22, 7]
    }],
    chart: {
      //width:150,
      height: 105,
      type: 'area',
      sparkline: {
        enabled: !0
      },
      zoom: {
        enabled: false
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      width: 1.7,
      curve: 'smooth'
    },
    fill: {
      type: 'gradient',
      gradient: {
        shade: 'dark',
        gradientToColors: ['#02c27a'],
        shadeIntensity: 1,
        type: 'vertical',
        opacityFrom: 0.5,
        opacityTo: 0.0,
        //stops: [0, 100, 100, 100]
      },
    },

    colors: ["#02c27a"],
    tooltip: {
      theme: "dark",
      fixed: {
        enabled: !1
      },
      x: {
        show: !1
      },
      y: {
        title: {
          formatter: function (e) {
            return ""
          }
        }
      },
      marker: {
        show: !1
      }
    },
    xaxis: {
      categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep'],
    }
  };

  var chart = new ApexCharts(document.querySelector("#chart4"), options);
  chart.render();
      // chart 1

  var options = {
    series: [{
      name: "Net Sales",
      data: [4, 10, 25, 12, 25, 18, 40, 22, 7]
    }],
    chart: {
      //width:150,
      height: 105,
      type: 'area',
      sparkline: {
        enabled: !0
      },
      zoom: {
        enabled: false
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      width: 1.7,
      curve: 'smooth'
    },
    fill: {
      type: 'gradient',
      gradient: {
        shade: 'dark',
        gradientToColors: ['#02c27a'],
        shadeIntensity: 1,
        type: 'vertical',
        opacityFrom: 0.5,
        opacityTo: 0.0,
        //stops: [0, 100, 100, 100]
      },
    },

    colors: ["#02c27a"],
    tooltip: {
      theme: "dark",
      fixed: {
        enabled: !1
      },
      x: {
        show: !1
      },
      y: {
        title: {
          formatter: function (e) {
            return ""
          }
        }
      },
      marker: {
        show: !1
      }
    },
    xaxis: {
      categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep'],
    }
  };

  var chart = new ApexCharts(document.querySelector("#chart3"), options);
  chart.render();


  // chart 5

  var options = {
    series: [{
      name: "Net Sales",
      data: [4, 10, 25, 12, 25, 18, 40, 22, 7]
    }],
    chart: {
      //width:150,
      height: 115,
      type: 'area',
      sparkline: {
        enabled: !0
      },
      zoom: {
        enabled: false
      }
    },
    dataLabels: {
      enabled: false
    },
    stroke: {
      width: 1.7,
      curve: 'smooth'
    },
    fill: {
      type: 'gradient',
      gradient: {
        shade: 'dark',
        gradientToColors: ['#6610f2'],
        shadeIntensity: 1,
        type: 'vertical',
        opacityFrom: 0.5,
        opacityTo: 0.0,
        //stops: [0, 100, 100, 100]
      },
    },

    colors: ["#6610f2"],
    tooltip: {
      theme: "dark",
      fixed: {
        enabled: !1
      },
      x: {
        show: !1
      },
      y: {
        title: {
          formatter: function (e) {
            return ""
          }
        }
      },
      marker: {
        show: !1
      }
    },
    xaxis: {
      categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep'],
    }
  };

  var chart = new ApexCharts(document.querySelector("#chart5"), options);
  chart.render();





});