using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFClientV2
{
    /// <summary>
    /// Serve para obter os dados exteriores da API da VOST sobre o COVID
    /// </summary>
    public class Root
    {
        public string data { get; set; }
        public string data_dados { get; set; }
        public int confirmados { get; set; }
        public int confirmados_arsnorte { get; set; }
        public int confirmados_arscentro { get; set; }
        public int confirmados_arslvt { get; set; }
        public int confirmados_arsalentejo { get; set; }
        public int confirmados_arsalgarve { get; set; }
        public int confirmados_acores { get; set; }
        public int confirmados_madeira { get; set; }
        public object confirmados_estrangeiro { get; set; }
        public int confirmados_novos { get; set; }
        public int recuperados { get; set; }
        public int obitos { get; set; }
        public int internados { get; set; }
        public int internados_uci { get; set; }
        public object lab { get; set; }
        public object suspeitos { get; set; }
        public int vigilancia { get; set; }
        public object n_confirmados { get; set; }
        public object cadeias_transmissao { get; set; }
        public object transmissao_importada { get; set; }
        public int confirmados_0_9_f { get; set; }
        public int confirmados_0_9_m { get; set; }
        public int confirmados_10_19_f { get; set; }
        public int confirmados_10_19_m { get; set; }
        public int confirmados_20_29_f { get; set; }
        public int confirmados_20_29_m { get; set; }
        public int confirmados_30_39_f { get; set; }
        public int confirmados_30_39_m { get; set; }
        public int confirmados_40_49_f { get; set; }
        public int confirmados_40_49_m { get; set; }
        public int confirmados_50_59_f { get; set; }
        public int confirmados_50_59_m { get; set; }
        public int confirmados_60_69_f { get; set; }
        public int confirmados_60_69_m { get; set; }
        public int confirmados_70_79_f { get; set; }
        public int confirmados_70_79_m { get; set; }
        public int confirmados_80_plus_f { get; set; }
        public int confirmados_80_plus_m { get; set; }
        public object sintomas_tosse { get; set; }
        public object sintomas_febre { get; set; }
        public object sintomas_dificuldade_respiratoria { get; set; }
        public object sintomas_cefaleia { get; set; }
        public object sintomas_dores_musculares { get; set; }
        public object sintomas_fraqueza_generalizada { get; set; }
        public int confirmados_f { get; set; }
        public int confirmados_m { get; set; }
        public int obitos_arsnorte { get; set; }
        public int obitos_arscentro { get; set; }
        public int obitos_arslvt { get; set; }
        public int obitos_arsalentejo { get; set; }
        public int obitos_arsalgarve { get; set; }
        public int obitos_acores { get; set; }
        public int obitos_madeira { get; set; }
        public object obitos_estrangeiro { get; set; }
        public object recuperados_arsnorte { get; set; }
        public object recuperados_arscentro { get; set; }
        public object recuperados_arslvt { get; set; }
        public object recuperados_arsalentejo { get; set; }
        public object recuperados_arsalgarve { get; set; }
        public object recuperados_acores { get; set; }
        public object recuperados_madeira { get; set; }
        public object recuperados_estrangeiro { get; set; }
        public int obitos_0_9_f { get; set; }
        public int obitos_0_9_m { get; set; }
        public int obitos_10_19_f { get; set; }
        public int obitos_10_19_m { get; set; }
        public int obitos_20_29_f { get; set; }
        public int obitos_20_29_m { get; set; }
        public int obitos_30_39_f { get; set; }
        public int obitos_30_39_m { get; set; }
        public int obitos_40_49_f { get; set; }
        public int obitos_40_49_m { get; set; }
        public int obitos_50_59_f { get; set; }
        public int obitos_50_59_m { get; set; }
        public int obitos_60_69_f { get; set; }
        public int obitos_60_69_m { get; set; }
        public int obitos_70_79_f { get; set; }
        public int obitos_70_79_m { get; set; }
        public int obitos_80_plus_f { get; set; }
        public int obitos_80_plus_m { get; set; }
        public int obitos_f { get; set; }
        public int obitos_m { get; set; }
        public object confirmados_desconhecidos_m { get; set; }
        public object confirmados_desconhecidos_f { get; set; }
        public int ativos { get; set; }
        public int internados_enfermaria { get; set; }
        public int confirmados_desconhecidos { get; set; }
        public object incidencia_nacional { get; set; }
        public object incidencia_continente { get; set; }
        public object rt_nacional { get; set; }
        public object rt_continente { get; set; }
        public string status { get; set; }

    }
}
