using System;
using Xamarin.Forms;
using System.Collections;
using System.Diagnostics;


namespace NapsackProblem
{
	public class App : Application
	{
		//MARK: Properties

		//main stack layout 
		StackLayout mainStackLayout = new StackLayout {
			Orientation = StackOrientation.Vertical,
			Padding = new Thickness (5,30,5,5),
			Spacing = 10,
			VerticalOptions = LayoutOptions.FillAndExpand
		};

		//entry point visual elements
		Label labelAmount = new Label {Text = "How many item do you have ?"};
		Label labelWeight = new Label {Text = "What is capacity of the Napsack (kg) ?"};
		Entry numberOfItems = new Entry {Placeholder = "Number of itmes:"};
		Entry weightLimit = new Entry {Placeholder = "Capacity of the Napsack:"};
		Button okButton1 = new Button {Text = "START", BorderWidth = 1, BorderColor = Color.Blue};
		Button okButton2 = new Button {Text = "COUNT", BorderWidth = 1, BorderColor = Color.Blue};

		//dynamically assigned arrays
		int[] itemsWeights;
		int[] itemsValues;

		Entry[] entryWeights;
		Entry[] entryValues;

		//asign activity on click


		//Constructor
		public App ()
		{

		okButton1.Clicked += fillData;
		okButton2.Clicked += readFromDataFromEntries;
	    okButton2.Clicked += createArrayOfsolutions;

		

		//main modul with the content
		MainPage = new ContentPage {
				Content = new ScrollView {
				  Content = mainStackLayout
				  }  
		 };

		//adding views entry point visual elements 
		mainStackLayout.Children.Add(labelAmount);
		mainStackLayout.Children.Add(numberOfItems);
		mainStackLayout.Children.Add(labelWeight);
		mainStackLayout.Children.Add(weightLimit);
		mainStackLayout.Children.Add(okButton1);
		 
		}//end of App constractor

		//MARK: Methods

		//filling data about items
		void fillData(object o, EventArgs e) {
		    //reading number of item and converting into type Int 
			int nOfelements = int.Parse(numberOfItems.Text);

			entryWeights = new Entry[nOfelements];
			entryValues = new Entry[nOfelements];

			//implement data input

			for (int i=0;i<nOfelements;++i) {
				entryWeights [i] = new Entry { Placeholder = "Item" + (i + 1) + " weight" };
				entryValues [i] = new Entry { Placeholder = "Item" + (i + 1) + " value" };

				mainStackLayout.Children.Add(new Label{Text = "Item "+(i+1)+" (weight, value)"});
				mainStackLayout.Children.Add(entryWeights[i]);	
				mainStackLayout.Children.Add(entryValues[i]);
			};

			//click to perform calculation
			mainStackLayout.Children.Add(okButton2);
			
			
		}//end of "fillData" method

		//method to read data about all items
		void readFromDataFromEntries(object o, EventArgs e) {
		    
			//determin amount of items
			int nOfelements = entryWeights.Length;
			 //initializing arrays where we will read all data about each item
			 itemsWeights = new int[nOfelements];
			 itemsValues =  new int[nOfelements];

			   for (int i=0;i<nOfelements;++i) {
				 itemsWeights [i] = int.Parse(entryWeights [i].Text);
				 itemsValues [i] = int.Parse(entryValues [i].Text);
	     	 	}
				
		}//end of "readFromDataFromEntries" method

		//method to produce the calculation
		void createArrayOfsolutions(object o, EventArgs e) {
            
			//read the capacity of the napsack
			int knapsackMaxCapacity = int.Parse(weightLimit.Text);
			int nOfelements = itemsWeights.Length;

			//creating two-diminetional array
			int [,] results = new int[nOfelements+1,knapsackMaxCapacity+1]; 
		    
			//filling the two-dimentional array using dynamic programming method
			for (int n=0;n<=knapsackMaxCapacity;++n) {results [0, n] = 0;}
			for (int s=1;s<=nOfelements;++s) {
			   for (int n = 0;n<=knapsackMaxCapacity;++n) {
				 results[s,n] = results[s-1,n];
					if (n>=itemsWeights[s-1] && (results[s-1,n-itemsWeights[s-1]]+itemsValues[s-1])>results[s,n] ) {
						
						results [s, n] = results [s - 1, n - itemsWeights [s - 1]] + itemsValues [s - 1];
				 } 
			   } 

			}

		 //call for the method to find an optimal set of items
			mainStackLayout.Children.Add(new Label{Text = "You should take:"});
			findSolution (nOfelements,knapsackMaxCapacity,results);
				
		 }//end of "CreateArrayOfsolutions" method

		//finding optimal set of item using recursive method
		void findSolution (int s, int n, int[,] results) {
			if (results [s, n] == 0) {
				return;
			}
			else if (results[s-1,n] ==  results[s,n]) {
				findSolution (s-1, n, results);
			}
			else {
				mainStackLayout.Children.Add(new Label{Text = "Item "+s+" (weight "+itemsWeights[s-1] + ", value "+itemsValues[s-1]+")"});
				//Debug.WriteLine ("Item "+s+" (weight "+itemsWeights[s-1] + ", value "+itemsValues[s-1]+")");
				findSolution (s-1, n-itemsWeights[s-1], results);
			}
		}//end of "findSolution" method
			

	 }//end of App

}//end of namespace NapsackProblem  

