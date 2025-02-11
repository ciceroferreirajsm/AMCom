using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Questao2.Domain;
public class FootballMatch
{
    public string Competition { get; set; }
    public int Year { get; set; }
    public string Round { get; set; }

    public string Team1 { get; set; }
    public string Team2 { get; set; }

    [JsonProperty("team1goals")]
    public int Team1Goals { get; set; }

    [JsonProperty("team2goals")]
    public int Team2Goals { get; set; }
}

public class ApiResponse
{
    public int Page { get; set; }

    [JsonProperty("per_page")]
    public int PerPage { get; set; }

    public int Total { get; set; }

    [JsonProperty("total_pages")]
    public int TotalPages { get; set; }

    public List<FootballMatch> Data { get; set; }
}
