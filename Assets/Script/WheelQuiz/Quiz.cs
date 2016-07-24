using System;
using System.Collections.Generic;


public class Quiz
{
	public String Category {
		get;
		set;
	}

	public String Question {
		get;
		set;
	}

	public List<String> Answers {
		get;
		set;
	}

	public Quiz (String category, String question, String answer1, String answer2, String answer3, String answer4)
	{
		this.Category = category;
		this.Question = question;
		this.Answers = new List<String> ();
		this.Answers.Add (answer1);
		this.Answers.Add (answer2);
		this.Answers.Add (answer3);
		this.Answers.Add (answer4);
	}
}

