using System;
using System.Collections.Generic;


public class QuizApp
{
	private List<Quiz> playedQuizes = new List<Quiz> ();
	private List<Quiz> quizes = new List<Quiz> ();
	private static QuizApp app = null;
	public static string Group {
		get;
		set;
	}
		
	public static QuizApp getInstance ()
	{
		if (app == null)
			app = new QuizApp ();

		return app;
	}

	public Quiz nextQuiz ()
	{
		return getQuizByCategory (Group);
	}


	private QuizApp ()
	{
		fillQuestions ();
	}



	public void NewGame ()
	{
		playedQuizes.Clear ();	
	}

	List<Quiz> filterQuizByCategory (List<Quiz> list, String category)
	{
		List<Quiz> result = new List<Quiz> ();

		foreach (var quiz in list) {
			if (quiz.Category.Equals (category))
				result.Add (quiz);
		}

		return result;
	}

	public Quiz getQuizByCategory (String category)
	{
		Quiz result = null;
		List<Quiz> quizes = filterQuizByCategory (this.quizes, category);

		// check if all questions was played
		if (filterQuizByCategory (playedQuizes, category).Count == 4) {
			playedQuizes.RemoveAll (quiz => quiz.Category.Equals (category));
		}

		Random random;
		int next;

		int i = 0;
		while (i < 4 && quizes.Count > 0) {
			random = new Random ();
			next = random.Next (quizes.Count);
			i++;
			Quiz quiz = quizes.ToArray () [next];
			if (playedQuizes.Contains (quiz)) {
				continue;
			} else {
				result = quiz;
				break;
			}
		}

		return result;
	}


	void fillQuestions ()
	{
		// Voeding
		quizes.Add (new Quiz ("Voeding", 
			"Wat eet een varken niet?", 
			"Chocola",
			"Frietjes",
			"Hij eet alle antwoorden",
			"Mais"));

		quizes.Add (new Quiz ("Voeding", 
			"Hoeveel kilo voer eet een volwassen varken per dag?", 
			"Tussen de 2 en 4 kilo",
			"Tussen de 10 en 4 kilo",
			"Tussen de 20 en 4 kilo",
			"Tussen de 1 en 2 kilo"));

		quizes.Add (new Quiz ("Voeding", 
			"Hoelang zeugt een biggetje (drinkt moedermelk)?", 
			"4 weken",
			"8 weken",
			"9 maanden",
			"1 week"));
		
		quizes.Add (new Quiz ("Voeding", 
			"Hoeveel kilo voer is nodig voor een kilo vlees?", 
			"2,6 kilo voer per kilo vlees",
			"10 kilo voer per kilo vlees",
			"5,5 kilo voer per kilo vlees",
			"1 kilo voer voor 1 kilo vlees"));

		// Huisvesting
		quizes.Add (new Quiz ("Huisvesting", 
			"Hoeveel m2 ruimte moet een volwassen vleesvarken minimaal hebben?", 
			"1 m2",
			"3 m2",
			"0,5 m2",
			"7 m2"));

		quizes.Add (new Quiz ("Huisvesting", 
			"Mag de vloer van een stal uit spijlen bestaan?", 
			"Ja, voor 60%",
			"Ja, helemaal mag",
			"Ja, voor 40%",
			"Nee"));

		quizes.Add (new Quiz ("Huisvesting", 
			"Hoeveel uur per dag moet het licht zijn in de stal?", 
			"8 uur per dag",
			"12 uur per dag",
			"24 uur per dag",
			"4 uur per dag"));

		quizes.Add (new Quiz ("Huisvesting", 
			"Hoelang mag een vleesvarken in zijn eentje in een hok zitten?", 
			"helemaal niet",
			"4 uur per dag",
			"zoveel als de boer wil",
			"het moet altijd"));

		// Varken zelf
		quizes.Add (new Quiz ("Varken zelf", 
			"Hoeveel biggetjes krijgt een zeug gemiddeld?", 
			"12",
			"8",
			"3",
			"20"));

		quizes.Add (new Quiz ("Varken zelf", 
			"Hoe zwaar is een varken als hij/zij naar de slacht gaat?", 
			"115 kilogram",
			"50 kilogram",
			"150 kilogram",
			"70 kilogram"));

		quizes.Add (new Quiz ("Varken zelf", 
			"Hoe heet een mannetjes varken?", 
			"een Beer",
			"een Zeug",
			"een Big",
			"een Hengst"));

		quizes.Add (new Quiz ("Varken zelf", 
			"Hoelang is een zeug drachtig (zwanger)?", 
			"3 maanden, 3 weken en 3 dagen",
			"9 maanden",
			"één jaar",
			"6 maanden"));

		// Emoties en omgang
		quizes.Add (new Quiz ("Emoties en omgang", 
			"Wat vindt een varken niet leuk?", 
			"op zichzelf zijn",
			"Spelen",
			"Plassen",
			"onbeperkt eten en drinken"));

		quizes.Add (new Quiz ("Emoties en omgang", 
			"Hoe herkennen varkens elkaar?", 
			"Door de geur",
			"Door hoe ze eruit zien",
			"Door het geluid wat ze maken",
			"Door een dansje"));

		quizes.Add (new Quiz ("Emoties en omgang", 
			"Wat vind een varken niet fijn als hij/zij het warm heeft?", 
			"het varken besproeien met water",
			"het varken water laten drinken",
			"voor veel ventilatie zorgen",
			"warm voer laten afkoelen"));

		quizes.Add (new Quiz ("Emoties en omgang", 
			"Hoe begroeten varkens elkaar?", 
			"door te neuzen",
			"door een boks te geven",
			"door met hun konten tegen elkaar te schuren",
			"door aan elkaars staart te snuffelen"));

		// Varkensproducten
		quizes.Add (new Quiz ("Varkensproducten", 
			"Hoeveel Kilo vlees komt er ongeveer van een varken?", 
			"54 kilogram",
			"115 kilogram",
			"100 kilogram",
			"23 kilogram"));

		quizes.Add (new Quiz ("Varkensproducten", 
			"Waarvan wordt o.a. lijm, bouillon en porselein gemaakt", 
			"van varkensbotten",
			"van varkenssnot",
			"van varkensoren",
			"van een varkensneus"));

		quizes.Add (new Quiz ("Varkensproducten", 
			"Wat wordt er gemaakt van varkenshuid?", 
			"leer, snoep en cosmetica producten",
			"cosmetica producten en medicijnen",
			"zeilen en windschermen",
			"alleen leer"));

		quizes.Add (new Quiz ("Varkensproducten", 
			"Waar worden varkensharen voor gebruikt?", 
			"voor het maken van kwasten",
			"voor het maken van Kleding",
			"voor het maken van papier",
			"voor het maken van pruiken"));
	}
		
}