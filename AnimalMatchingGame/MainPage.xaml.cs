namespace AnimalMatchingGame
{
	public partial class MainPage : ContentPage
	{
		
		public MainPage()
		{
			InitializeComponent();
		}

		private void PlayAgainButton_Clicked(object sender, EventArgs e)
		{
			AnimalButtons.IsVisible = true;
			PlayAgainButton.IsVisible = false;
			List<string> animalEmoji = [
				"🐵", "🐵",
				"🐶", "🐶",
				"🐺", "🐺",
				"🐱", "🐱",
				"🦁", "🦁",
				"🐯", "🐯",
				"🦒", "🦒",
				"🦊", "🦊"
				];
			foreach (var button in AnimalButtons.Children.OfType<Button>())
			{
				int index = Random.Shared.Next(animalEmoji.Count);
				string nextEmoji = animalEmoji[index];
				button.Text = nextEmoji;
				animalEmoji.RemoveAt(index);
			}
			Dispatcher.StartTimer(TimeSpan.FromSeconds(0.1), TimerTick);
		}

		int tenthsOfSecondsElapsed = 0;
		private bool TimerTick()
		{
			if (!this.IsLoaded) { return false; }
			tenthsOfSecondsElapsed++;
			TimeElapsed.Text = "Time Elapsed: " + (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
			if (PlayAgainButton.IsVisible)
			{
				tenthsOfSecondsElapsed = 0;
				return false;
			}
			return true;
		}

		Button lastClicked;
		bool findingMatch = false;
		int matchesFound;

		private void Button_Clicked(object sender, EventArgs e)
		{
			if (sender is Button buttonClicked)
			{
				if (!String.IsNullOrWhiteSpace(buttonClicked.Text) && (findingMatch == false))
				{
					buttonClicked.Background = Colors.Red;
					lastClicked = buttonClicked;
					findingMatch = true;
				}
				else
				{
					if ((buttonClicked != lastClicked) && (!String.IsNullOrWhiteSpace(buttonClicked.Text)) && (buttonClicked.Text == lastClicked.Text)) {
						matchesFound++;
						buttonClicked.Text = " ";
						lastClicked.Text = " ";
					}
					lastClicked.Background = Colors.LightBlue;
					buttonClicked.Background = Colors.LightBlue;
					findingMatch = false ;
				}

				if (matchesFound == 8)
				{
					matchesFound = 0;
					AnimalButtons.IsVisible = false;
					PlayAgainButton.IsVisible = true;
				}

			}
		}
	}
	 
}
