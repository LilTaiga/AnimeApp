using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

using AnimeApp.Classes.Anilist;

namespace AnimeApp.Classes
{
    public class SampleData
    {
        public ObservableCollection<AnilistResult.Entry> Data { get; set; }
        public SampleData()
        {
            if (DesignMode.DesignModeEnabled)
            {
                Data = new ObservableCollection<AnilistResult.Entry>()
            {
                new AnilistResult.Entry()
                {
                    mediaId = 0,                                     //Irrelevante pra vocês, é coisa interna do Anilist.
                    score = 0,                                       //A minha nota pro anime, escreva no formato XX.XX.
                    progress = 0,                                       //Quantos episódios eu já assisti.
                    repeat = 0,                                         //Quantas vezes eu já reassisti.
                    priority = 0,                                       //Irrelevante pra vocês, é coisa interna do Anilist.
                    createdAt = 0,                              //Irrelevante pra vocês, é coisa interna de programação.
                    updatedAt = 0,                             //Irrelevante pra vocês, é coisa interna de programação.
                    media = new AnilistResult.Media()
                    {
                        id = 0,
                        title = new AnilistResult.Title
                        {
                            english = "",
                            romaji = "",
                            native = "",
                            userPreferred = ""
                        },
                        format = "",                                 //TV, TV short, Movie, Special, OVA, ONA, Music, Manga, Novel, One shot
                        status = "",                           //Cancelled, Not yet released, Releasing, Finished
                        description = "",
                        startDate = new AnilistResult.StartDate{ day = 0, month = 0, year = 0 },
                        endDate = new AnilistResult.EndDate{ day = 0, month = 0, year = 0 },
                        season = "",                              //Spring, Winter, Summer, Fall
                        seasonInt = 0,                                  //1 = primeira season do ano, 2 = segunda season do ano, etc...
                        seasonYear = 0,
                        episodes = 0,
                        duration = 0,
                        coverImage = new AnilistResult.CoverImage
                        {
                            medium = "",           //PS: Pode ser um link da internet
                            large = "",            //PS: Pode ser um link da internet
                            extraLarge = "",       //PS: Pode ser um link da internet
                            color = ""                            //Irrelevante para vocês, a cor média HEX da imagem
                        },
                        genres = new List<string>()
                        {
                            ""
                        },
                        synonyms = new List<string>()
                        {
                            ""
                        },
                        averageScore = 100,
                        meanScore = 100,
                        popularity = 100,                                //Se refere ao ano.    "#XX Most popular 2017"
                        tags = new List<AnilistResult.Tag>()
                        {
                            new AnilistResult.Tag()
                            {
                                name = "",
                                rank = 0,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "",
                                isAdult = false,
                                id = 0                                //Irrelevante para vocês, é coisa interna do Anilist.
                            },
                            new AnilistResult.Tag()
                            {
                                name = "",
                                rank = 0,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "",
                                isAdult = false,
                                id = 0                                 //Irrelevante para vocês, é coisa interna do Anilist.
                            }
                        },
                        isAdult = false,
                        nextAiringEpisode = null,
                        siteUrl = "",                         //Se não tiver, deixe em branco.
                        externalLinks = new List<AnilistResult.ExternalLink>()
                        {
                            new AnilistResult.ExternalLink { url = "", site = "" }
                        }
                    }
                },

                new AnilistResult.Entry()
                {
                    mediaId = 7777,                                     //Irrelevante pra vocês, é coisa interna do Anilist.
                    score = 63.7,                                       //A minha nota pro anime, escreva no formato XX.XX.
                    progress = 7,                                       //Quantos episódios eu já assisti.
                    repeat = 3,                                         //Quantas vezes eu já reassisti.
                    priority = 5,                                       //Irrelevante pra vocês, é coisa interna do Anilist.
                    createdAt = 123456789,                              //Irrelevante pra vocês, é coisa interna de programação.
                    updatedAt = 1234567890,                             //Irrelevante pra vocês, é coisa interna de programação.
                    media = new AnilistResult.Media()
                    {
                        id = 12345,
                        title = new AnilistResult.Title
                        {
                            english = "Título 1",
                            romaji = "Título 2",
                            native = "Título 3",
                            userPreferred = "Título 2"
                        },
                        format = "OVA",                                 //TV, TV short, Movie, Special, OVA, ONA, Music, Manga, Novel, One shot
                        status = "Completed",                           //Cancelled, Not yet released, Releasing, Finished
                        description = "Lorem Ipsilum.",
                        startDate = new AnilistResult.StartDate{ day = 7, month = 7, year = 7 },
                        endDate = new AnilistResult.EndDate{ day = 7, month = 7, year = 7 },
                        season = "Spring",                              //Spring, Winter, Summer, Fall
                        seasonInt = 1,                                  //1 = primeira season do ano, 2 = segunda season do ano, etc...
                        seasonYear = 7,
                        episodes = 13,
                        duration = 24,
                        coverImage = new AnilistResult.CoverImage
                        {
                            medium = "MeuLinkParaImagem.png",           //PS: Pode ser um link da internet
                            large = "MeuLinkParaImagem.png",            //PS: Pode ser um link da internet
                            extraLarge = "MeuLinkParaImagem.png",       //PS: Pode ser um link da internet
                            color = "777777"                            //A cor média da imagem
                        },
                        genres = new List<string>()
                        {
                            "Action",
                            "Adventure",
                            "Drama"
                        },
                        synonyms = new List<string>()
                        {
                            "Título2",
                            "Título-2",
                            "Titulo 2",
                            "Til2"
                        },
                        averageScore = 83,
                        meanScore = 7,
                        popularity = 53,                                //Se refere ao ano.    "#XX Most popular 2017"
                        tags = new List<AnilistResult.Tag>()
                        {
                            new AnilistResult.Tag()
                            {
                                name = "Cute Girls Doing Cute Things",
                                rank = 100,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = true,                  //:framdying
                                category = "Theme/Slice of Life",
                                isAdult = false,
                                id = 777                                //Irrelevante para vocês, é coisa interna do Anilist.
                            }
                        },
                        isAdult = false,
                        nextAiringEpisode = new AnilistResult.NextAiringEpisode()
                        {
                            airingAt = 777,                             //Irrelevante pra vocês, é coisa interna de programação.
                            timeUntilAiring = 777,                      //Segundos que faltam até o próximo episódio.
                            episode = 3
                        },                                              //PS: Inclua apenas se o anime ainda estiver lançado, caso contrário, substitua pela linha abaixo.
                        //nextAiringEpisode = null,                     //PS: Apague o nextAiringEpisode anterior e remova os // do desse.
                        siteUrl = "google.com",                         //Caso o anime tenha um site oficial,
                        externalLinks = new List<AnilistResult.ExternalLink>()
                        {
                            new AnilistResult.ExternalLink { url = "crunchyroll.com/linkdoanime", site = "Crunchyroll" },
                            new AnilistResult.ExternalLink { url = "funimation.com/linkdoanime", site = "Funimation" },
                            new AnilistResult.ExternalLink { url = "twitter.com/linkdoanime", site = "Twitter" },
                            new AnilistResult.ExternalLink { url = "thepiratebay.com/linkdoanime", site = "Link pra baixar pirata :clandestino:"}
                        }
                    }
                },
                new AnilistResult.Entry()
                {
                    mediaId = 12345,                                     //Irrelevante pra vocês, é coisa interna do Anilist.
                    score = 100.0,                                       //A minha nota pro anime, escreva no formato XX.XX.
                    progress = 13,                                       //Quantos episódios eu já assisti.
                    repeat = 0,                                         //Quantas vezes eu já reassisti.
                    priority = 0,                                       //Irrelevante pra vocês, é coisa interna do Anilist.
                    createdAt = 123456789,                              //Irrelevante pra vocês, é coisa interna de programação.
                    updatedAt = 1234567890,                             //Irrelevante pra vocês, é coisa interna de programação.
                    media = new AnilistResult.Media()
                    {
                        id = 54321,
                        title = new AnilistResult.Title
                        {
                            english = "Tiger x Dragon",
                            romaji = "Toradora!",
                            native = "トラどら",
                            userPreferred = "トラどら"
                        },
                        format = "TV",                                 //TV, TV short, Movie, Special, OVA, ONA, Music, Manga, Novel, One shot
                        status = "Completed",                           //Cancelled, Not yet released, Releasing, Finished
                        description = "Preguiça de copiar a descrição aqui.",
                        startDate = new AnilistResult.StartDate{ day = 3, month = 5, year = 2010 },
                        endDate = new AnilistResult.EndDate{ day = 25, month = 12, year = 2011 },
                        season = "Fall",                              //Spring, Winter, Summer, Fall
                        seasonInt = 2,                                  //1 = primeira season do ano, 2 = segunda season do ano, etc...
                        seasonYear = 2010,
                        episodes = 25,
                        duration = 24,
                        coverImage = new AnilistResult.CoverImage
                        {
                            medium = "https://s4.anilist.co/file/anilistcdn/media/anime/cover/large/bx4224-ARdWFsE4yyKU.jpg",           //PS: Pode ser um link da internet
                            large = "https://s4.anilist.co/file/anilistcdn/media/anime/cover/large/bx4224-ARdWFsE4yyKU.jpg",            //PS: Pode ser um link da internet
                            extraLarge = "https://s4.anilist.co/file/anilistcdn/media/anime/cover/large/bx4224-ARdWFsE4yyKU.jpg",       //PS: Pode ser um link da internet
                            color = "#A753D3"                            //Irrelevante para vocês, a cor média HEX da imagem
                        },
                        genres = new List<string>()
                        {
                            "Comedy",
                            "Drama",
                            "Romance",
                            "Slice of Life"
                        },
                        synonyms = new List<string>()
                        {
                            "Tiger X Dragon"
                        },
                        averageScore = 80,
                        meanScore = 80,
                        popularity = 23,                                //Se refere ao ano.    "#XX Most popular 2017"
                        tags = new List<AnilistResult.Tag>()
                        {
                            new AnilistResult.Tag()
                            {
                                name = "Coming of Age",
                                rank = 93,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "Preguiça de procurar",
                                isAdult = false,
                                id = 777                                //Irrelevante para vocês, é coisa interna do Anilist.
                            },
                            new AnilistResult.Tag()
                            {
                                name = "School",
                                rank = 88,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "Preguiça de procurar",
                                isAdult = false,
                                id = 777                                //Irrelevante para vocês, é coisa interna do Anilist.
                            },
                            new AnilistResult.Tag()
                            {
                                name = "Tsundere",
                                rank = 88,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "Preguiça de procurar",
                                isAdult = false,
                                id = 777                                //Irrelevante para vocês, é coisa interna do Anilist.
                            },
                            new AnilistResult.Tag()
                            {
                                name = "Love Triangle",
                                rank = 81,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "Preguiça de procurar",
                                isAdult = false,
                                id = 777                                //Irrelevante para vocês, é coisa interna do Anilist.
                            }
                            //Então, deu preguiça de colocar o resto.
                        },
                        isAdult = false,
                        nextAiringEpisode = null,
                        siteUrl = "https://www.tv-tokyo.co.jp/anime/toradora/",                         //Caso o anime tenha um site oficial,
                        externalLinks = new List<AnilistResult.ExternalLink>()
                        {
                            new AnilistResult.ExternalLink { url = "https://www.amazon.com/dp/B07L7D9ZTJ/?&_encoding=UTF8&tag=anilist07-20&linkCode=ur2&linkId=e4eedff3506c0bb7d93239fedc6a41e2&camp=1789&creative=9325", site = "Amazon" },
                            new AnilistResult.ExternalLink { url = "https://www.crunchyroll.com/pt-br/toradora", site = "Crunchyroll" },
                            new AnilistResult.ExternalLink { url = "https://www.funimation.com/pt-br/out-of-territory/", site = "Funimation" },
                            new AnilistResult.ExternalLink { url = "https://www.netflix.com/br/title/80049275", site = "Netflix"}
                        }
                    }
                },


                new AnilistResult.Entry()
                {
                    mediaId = 7732,                                     //Irrelevante pra vocês, é coisa interna do Anilist.
                    score = 63.7,                                       //A minha nota pro anime, escreva no formato XX.XX.
                    progress = 7,                                       //Quantos episódios eu já assisti.
                    repeat = 3,                                         //Quantas vezes eu já reassisti.
                    priority = 5,                                       //Irrelevante pra vocês, é coisa interna do Anilist.
                    createdAt = 12345,                              //Irrelevante pra vocês, é coisa interna de programação.
                    updatedAt = 54321,                             //Irrelevante pra vocês, é coisa interna de programação.
                    media = new AnilistResult.Media()
                    {
                        id = 12345,
                        title = new AnilistResult.Title
                        {
                            english = "n seasonInt",
                            romaji = "idk",
                            native = "Títdsoihnsdoinhdsionhdsoiulo 3",
                            userPreferred = "abla2"
                        },
                        format = "Movie",                                 //TV, TV short, Movie, Special, OVA, ONA, Music, Manga, Novel, One shot
                        status = "Watching",                           //Cancelled, Not yet released, Releasing, Finished
                        description = "Lorem Ipsilumsaiongioan.",
                        startDate = new AnilistResult.StartDate{ day = 7, month = 7, year = 7 },
                        endDate = new AnilistResult.EndDate{ day = 7, month = 7, year = 7 },
                        season = "Fall",                              //Spring, Winter, Summer, Fall
                        seasonInt = 1,                                  //1 = primeira season do ano, 2 = segunda season do ano, etc...
                        seasonYear = 7,
                        episodes = 13,
                        duration = 24,
                        coverImage = new AnilistResult.CoverImage
                        {
                            medium = "https://www.atomix.com.au/media/2017/07/StockPhotoBanner.jpg",           //PS: Pode ser um link da internet
                            large = "https://www.atomix.com.au/media/2017/07/StockPhotoBanner.jpg",            //PS: Pode ser um link da internet
                            extraLarge = "https://www.atomix.com.au/media/2017/07/StockPhotoBanner.jpg",       //PS: Pode ser um link da internet
                            color = "000000"                            //A cor média da imagem
                        },
                        genres = new List<string>()
                        {
                            "Action",
                            "Slice of Life",
                            "Drama",
                            "Robots"
                        },
                        synonyms = new List<string>()
                        {
                            "dsadsdsadas",
                            "asdasdasdasdas",
                            "asdasdasdasdas"
                        },
                        averageScore = 53,
                        meanScore = 0,
                        popularity = 1,                                //Se refere ao ano.    "#XX Most popular 2017"
                        tags = new List<AnilistResult.Tag>()
                        {
                            new AnilistResult.Tag()
                            {
                                name = "Cute Girls Doing Horrible Things",
                                rank = 110,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = true,                  //:framdying
                                category = "Theme/Slice of Death",
                                isAdult = false,
                                id = 777                                //Irrelevante para vocês, é coisa interna do Anilist.
                            },
                            new AnilistResult.Tag()
                            {
                                name = "asdassadsadsa",
                                rank = 505,                             //A porcentagem da tag
                                isGeneralSpoiler = true,
                                isMediaSpoiler = true,                  //:framdying
                                category = "Theme/Slice of Death",
                                isAdult = false,
                                id = 777                                //Irrelevante para vocês, é coisa interna do Anilist.
                            }
                        },
                        isAdult = false,
                        nextAiringEpisode = new AnilistResult.NextAiringEpisode()
                        {
                            airingAt = 777,                             //Irrelevante pra vocês, é coisa interna de programação.
                            timeUntilAiring = 3,                      //Segundos que faltam até o próximo episódio.
                            episode = 3
                        },                                              //PS: Inclua apenas se o anime ainda estiver lançado, caso contrário, substitua pela linha abaixo.
                        //nextAiringEpisode = null,                     //PS: Apague o nextAiringEpisode anterior e remova os // do desse.
                        siteUrl = "http://dan-ball.jp/en/",                         //Caso o anime tenha um site oficial,
                        externalLinks = new List<AnilistResult.ExternalLink>()
                        {
                            new AnilistResult.ExternalLink { url = "crunchyroll.com/mole", site = "Crunchyroll" },
                            new AnilistResult.ExternalLink { url = "funimation.com/molissimo", site = "Funimation" },
                            new AnilistResult.ExternalLink { url = "twitter.com/molento", site = "Twitter" },
                            new AnilistResult.ExternalLink { url = "thepiratebay.com/molasso", site = "Link pra baixar pirata :clandestino:"}
                        }
                    }
                },
                //ahm oq é pra eu fazer foda

                new AnilistResult.Entry()
                {
                    //Vou inventar algo, não precisa fazer sentido
                    mediaId = 0,                                     //Irrelevante pra vocês, é coisa interna do Anilist.
                    score = -73,                                       //A minha nota pro anime, escreva no formato XX.XX.
                    progress = 999,                                       //Quantos episódios eu já assisti.
                    repeat = 8,                                         //Quantas vezes eu já reassisti.
                    priority = 123123,                                       //Irrelevante pra vocês, é coisa interna do Anilist.
                    createdAt = 0,                              //Irrelevante pra vocês, é coisa interna de programação.
                    updatedAt = 0,                             //Irrelevante pra vocês, é coisa interna de programação.
                    media = new AnilistResult.Media()
                    {
                        id = 0505,
                        title = new AnilistResult.Title
                        {
                            english = "Comi o cu de quem tá lendo",
                            romaji = "Comi o cu de quem tá lendo",
                            native = "Comi o cu de quem tá lendo",
                            userPreferred = "Comi o cu de quem tá lendo"
                        },
                        format = "Music",                                 //TV, TV short, Movie, Special, OVA, ONA, Music, Manga, Novel, One shot
                        status = "Completed",                           //Cancelled, Not yet released, Releasing, Finished
                        description = "Preguiça de copiar a descrição aqui.",
                        startDate = new AnilistResult.StartDate{ day = 31, month = 12, year = 2020 },
                        endDate = new AnilistResult.EndDate{ day = 31, month = 12, year = 2020 },
                        season = "Winter",                              //Spring, Winter, Summer, Fall
                        seasonInt = 4,                                  //1 = primeira season do ano, 2 = segunda season do ano, etc...
                        seasonYear = 2020,
                        episodes = 1,
                        duration = 5,
                        coverImage = new AnilistResult.CoverImage
                        {
                            medium = "https://s4.anilist.co/file/anilistcdn/media/anime/cover/large/bx4224-ARdWFsE4yyKU.jpg",           //PS: Pode ser um link da internet
                            large = "https://s4.anilist.co/file/anilistcdn/media/anime/cover/large/bx4224-ARdWFsE4yyKU.jpg",            //PS: Pode ser um link da internet
                            extraLarge = "https://s4.anilist.co/file/anilistcdn/media/anime/cover/large/bx4224-ARdWFsE4yyKU.jpg",       //PS: Pode ser um link da internet
                            color = "#A753D3"                            //Irrelevante para vocês, a cor média HEX da imagem
                        },
                        genres = new List<string>()
                        {
                            "Action",
                            "Adventure",
                            "Comedy",
                            "Ecchi",
                            "Hentai"
                        },
                        synonyms = new List<string>()
                        {
                            "asdf",
                            "asdf",
                            "asdf",
                            "asdf"
                        },
                        averageScore = 100,
                        meanScore = 100,
                        popularity = 100,                                //Se refere ao ano.    "#XX Most popular 2017"
                        tags = new List<AnilistResult.Tag>()
                        {
                            new AnilistResult.Tag()
                            {
                                name = "Tragedy",
                                rank = 100,                             //A porcentagem da tag
                                isGeneralSpoiler = true,
                                isMediaSpoiler = false,                  //:framdying
                                category = "Preguiça de procurar",
                                isAdult = false,
                                id = 777                                //Irrelevante para vocês, é coisa interna do Anilist.
                            },
                            new AnilistResult.Tag()
                            {
                                name = "Mahjong",
                                rank = 8,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "Preguiça de procurar",
                                isAdult = false,
                                id = 777                                //Irrelevante para vocês, é coisa interna do Anilist.
                            },
                            new AnilistResult.Tag()
                            {
                                name = "Iyashikei",
                                rank = 30,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "Preguiça de procurar",
                                isAdult = false,
                                id = 777                                //Irrelevante para vocês, é coisa interna do Anilist.
                            }
                        },
                        isAdult = true,
                        nextAiringEpisode = null,
                        siteUrl = "",                         //Se não tiver, deixe em branco.
                        externalLinks = new List<AnilistResult.ExternalLink>()
                        {
                            new AnilistResult.ExternalLink { url = "https://youtu.be/dQw4w9WgXcQ", site = "Youtube" }
                        }
                    }
                },

                new AnilistResult.Entry()
                {
                    mediaId = 0,                                     //Irrelevante pra vocês, é coisa interna do Anilist.
                    score = 0,                                       //A minha nota pro anime, escreva no formato XX.XX.
                    progress = 7,                                       //Quantos episódios eu já assisti.
                    repeat = 0,                                         //Quantas vezes eu já reassisti.
                    priority = 0,                                       //Irrelevante pra vocês, é coisa interna do Anilist.
                    createdAt = 0,                              //Irrelevante pra vocês, é coisa interna de programação.
                    updatedAt = 0,                             //Irrelevante pra vocês, é coisa interna de programação.
                    media = new AnilistResult.Media()
                    {
                        id = 0,
                        title = new AnilistResult.Title
                        {
                            english = "uai",
                            romaji = "oxe",
                            native = "あた",
                            userPreferred = "おｋ"
                        },
                        format = "TV short",                                 //TV, TV short, Movie, Special, OVA, ONA, Music, Manga, Novel, One shot
                        status = "Cancelled",                           //Cancelled, Not yet released, Releasing, Finished
                        description = "",
                        startDate = new AnilistResult.StartDate{ day = 0, month = 0, year = 0 },
                        endDate = new AnilistResult.EndDate{ day = 0, month = 0, year = 0 },
                        season = "Spring",                              //Spring, Winter, Summer, Fall
                        seasonInt = 0,                                  //1 = primeira season do ano, 2 = segunda season do ano, etc...
                        seasonYear = 0,
                        episodes = 0,
                        duration = 0,
                        coverImage = new AnilistResult.CoverImage
                        {
                            medium = "https://media.discordapp.net/attachments/472313197836107780/651576210194956298/3spsiuX.png",           //PS: Pode ser um link da internet
                            large = "https://media.discordapp.net/attachments/472313197836107780/651576210194956298/3spsiuX.png",            //PS: Pode ser um link da internet
                            extraLarge = "https://media.discordapp.net/attachments/472313197836107780/651576210194956298/3spsiuX.png",       //PS: Pode ser um link da internet
                            color = "FF00FF"                            //Irrelevante para vocês, a cor média HEX da imagem
                        },
                        genres = new List<string>()
                        {
                            "genero numero 1",
                            "genero number 2",
                            "number fifteen burger king foot lettuce"
                        },
                        synonyms = new List<string>()
                        {
                            "omsdgnkladsgnl",
                            "eu acho"
                        },
                        averageScore = 100,
                        meanScore = 100,
                        popularity = 100,                                //Se refere ao ano.    "#XX Most popular 2017"
                        tags = new List<AnilistResult.Tag>()
                        {
                            new AnilistResult.Tag()
                            {
                                name = "Badulhos",
                                rank = 69,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "sdgsd",
                                isAdult = false,
                                id = 0                                //Irrelevante para vocês, é coisa interna do Anilist.
                            },
                            new AnilistResult.Tag()
                            {
                                name = "gsdgsdgsd",
                                rank = 0,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "gfdkyutglul",
                                isAdult = false,
                                id = 0                                 //Irrelevante para vocês, é coisa interna do Anilist.
                            }
                        },
                        isAdult = false,
                        nextAiringEpisode = null,
                        siteUrl = "https://codecollab.io/@proj/StoryGuitarCity",                         //Se não tiver, deixe em branco.
                        externalLinks = new List<AnilistResult.ExternalLink>()
                        {
                            new AnilistResult.ExternalLink { url = "https://codecollab.io/@proj/StoryGuitarCity", site = "https://codecollab.io/@proj/StoryGuitarCity" }
                        }
                    }
                },

                new AnilistResult.Entry()
                {
                    mediaId = 0,                                     //Irrelevante pra vocês, é coisa interna do Anilist.
                    score = 0,                                       //A minha nota pro anime, escreva no formato XX.XX.
                    progress = 0,                                       //Quantos episódios eu já assisti.
                    repeat = 0,                                         //Quantas vezes eu já reassisti.
                    priority = 0,                                       //Irrelevante pra vocês, é coisa interna do Anilist.
                    createdAt = 0,                              //Irrelevante pra vocês, é coisa interna de programação.
                    updatedAt = 0,                             //Irrelevante pra vocês, é coisa interna de programação.
                    media = new AnilistResult.Media()
                    {
                        id = 0,
                        title = new AnilistResult.Title
                        {
                            english = "Testing w/ Mock Data",
                            romaji = "testingu withu mocki deita",
                            native = "insert moonrunes here",
                            userPreferred = "User moonrunes"
                        },
                        format = "TV",                                 //TV, TV short, Movie, Special, OVA, ONA, Music, Manga, Novel, One shot
                        status = "Releasing",                           //Cancelled, Not yet released, Releasing, Finished
                        description = "uwu",
                        startDate = new AnilistResult.StartDate{ day = 0, month = 0, year = 0 },
                        endDate = new AnilistResult.EndDate{ day = 0, month = 0, year = 0 },
                        season = "Fall",                              //Spring, Winter, Summer, Fall
                        seasonInt = 5,                                  //1 = primeira season do ano, 2 = segunda season do ano, etc...
                        seasonYear = 2,
                        episodes = 99999,
                        duration = 9999999,
                        coverImage = new AnilistResult.CoverImage
                        {
                            medium = "https://codecollab.io/@proj/StoryGuitarCity",           //PS: Pode ser um link da internet
                            large = "https://codecollab.io/@proj/StoryGuitarCity",            //PS: Pode ser um link da internet
                            extraLarge = "https://codecollab.io/@proj/StoryGuitarCity",       //PS: Pode ser um link da internet
                            color = "FF00FF"                            //Irrelevante para vocês, a cor média HEX da imagem
                        },
                        genres = new List<string>()
                        {
                            "genro",
                            "nora",
                            "reimu"
                        },
                        synonyms = new List<string>()
                        {
                            "SELECT * FROM Users WHERE * = \\"
                        },
                        averageScore = 100,
                        meanScore = 100,
                        popularity = 100,                                //Se refere ao ano.    "#XX Most popular 2017"
                        tags = new List<AnilistResult.Tag>()
                        {
                            new AnilistResult.Tag()
                            {
                                name = "Badulhos",
                                rank = 69,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "sdgsd",
                                isAdult = false,
                                id = 0                                //Irrelevante para vocês, é coisa interna do Anilist.
                            },
                            new AnilistResult.Tag()
                            {
                                name = "gsdgsdgsd",
                                rank = 0,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "gfdkyutglul",
                                isAdult = false,
                                id = 0                                 //Irrelevante para vocês, é coisa interna do Anilist.
                            }
                        },
                        isAdult = false,
                        nextAiringEpisode = null,
                        siteUrl = "https://codecollab.io/@proj/StoryGuitarCity",                         //Se não tiver, deixe em branco.
                        externalLinks = new List<AnilistResult.ExternalLink>()
                        {
                            new AnilistResult.ExternalLink { url = "https://codecollab.io/@proj/StoryGuitarCity", site = "https://codecollab.io/@proj/StoryGuitarCity" }
                        }
                    }
                },

                new AnilistResult.Entry()
                {
                    mediaId = 0,                                     //Irrelevante pra vocês, é coisa interna do Anilist.
                    score = 0,                                       //A minha nota pro anime, escreva no formato XX.XX.
                    progress = 7,                                       //Quantos episódios eu já assisti.
                    repeat = 0,                                         //Quantas vezes eu já reassisti.
                    priority = 0,                                       //Irrelevante pra vocês, é coisa interna do Anilist.
                    createdAt = 0,                              //Irrelevante pra vocês, é coisa interna de programação.
                    updatedAt = 0,                             //Irrelevante pra vocês, é coisa interna de programação.
                    media = new AnilistResult.Media()
                    {
                        id = 0,
                        title = new AnilistResult.Title
                        {
                            english = "uredshrethai",
                            romaji = "oreshrehrexe",
                            native = "あhdfhjdfhdfた",
                            userPreferred = "hgdfhdfhfdおｋ"
                        },
                        format = "ONA",                                 //TV, TV short, Movie, Special, OVA, ONA, Music, Manga, Novel, One shot
                        status = "Releasing",                           //Cancelled, Not yet released, Releasing, Finished
                        description = "mt bomn",
                        startDate = new AnilistResult.StartDate{ day = 0, month = 0, year = 0 },
                        endDate = new AnilistResult.EndDate{ day = 0, month = 0, year = 0 },
                        season = "Spring",                              //Spring, Winter, Summer, Fall
                        seasonInt = 0,                                  //1 = primeira season do ano, 2 = segunda season do ano, etc...
                        seasonYear = 0,
                        episodes = 0,
                        duration = 0,
                        coverImage = new AnilistResult.CoverImage
                        {
                            medium = "https://images-ext-1.discordapp.net/external/aHlhBgKbdnF1oyVtF0X-0Cj9Dd0JzQ2aqHMeWJWGwnA/https/media.discordapp.net/attachments/472313197836107780/650906113893597197/o1So3UP.png",           //PS: Pode ser um link da internet
                            large = "https://images-ext-1.discordapp.net/external/-pJ8V1wkkRtqp8I3tTFYSiqer5uW6UVECBbNfI7A5LQ/https/pbs.twimg.com/media/Esbv_ttXIAAql49.jpg",            //PS: Pode ser um link da internet
                            extraLarge = "https://images-ext-1.discordapp.net/external/aHlhBgKbdnF1oyVtF0X-0Cj9Dd0JzQ2aqHMeWJWGwnA/https/media.discordapp.net/attachments/472313197836107780/650906113893597197/o1So3UP.png",       //PS: Pode ser um link da internet
                            color = "FF00FF"                            //Irrelevante para vocês, a cor média HEX da imagem
                        },
                        genres = new List<string>()
                        {
                            "genero nbgsdbsdbsdbsdbumero 1",
                            "generosdbgsdnumber 2",
                            "number sdbdsbfifteen burger king foot lettuce"
                        },
                        synonyms = new List<string>()
                        {
                            "omsdgnkla.kj;jk.jk.,dsgnl",
                            "eu asdbdshcho"
                        },
                        averageScore = 100,
                        meanScore = 100,
                        popularity = 100,                                //Se refere ao ano.    "#XX Most popular 2017"
                        tags = new List<AnilistResult.Tag>()
                        {
                            new AnilistResult.Tag()
                            {
                                name = "Badusdhdshsdhdslhos",
                                rank = 6969,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "shsdhsdhsdgsd",
                                isAdult = false,
                                id = 0                                //Irrelevante para vocês, é coisa interna do Anilist.
                            },
                            new AnilistResult.Tag()
                            {
                                name = "gsuwuuwuwuwuwuwudgsdgsd",
                                rank = 0,                             //A porcentagem da tag
                                isGeneralSpoiler = false,
                                isMediaSpoiler = false,                  //:framdying
                                category = "gfdkyutglul",
                                isAdult = false,
                                id = 0                                 //Irrelevante para vocês, é coisa interna do Anilist.
                            }
                        },
                        isAdult = false,
                        nextAiringEpisode = null,
                        siteUrl = "https://codecollab.io/@proj/StoryGuitarCity",                         //Se não tiver, deixe em branco.
                        externalLinks = new List<AnilistResult.ExternalLink>()
                        {
                            new AnilistResult.ExternalLink { url = "https://codecollab.io/@proj/StoryGuitarCity", site = "https://codecollab.io/@proj/StoryGuitarCity" }
                        }
                    }
                }
            };
            }
        }
    }
}
