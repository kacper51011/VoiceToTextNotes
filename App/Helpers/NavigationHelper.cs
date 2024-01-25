using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Helpers
{
    public class NavigationHelper
    {
		/// <summary>
		/// Reusable method that simplify navigation logic
		/// </summary>
		/// <param name="url">Url to navigate to</param>
		/// <param name="commands">Commands to refresh</param>
		/// <returns></returns>
        public static async Task NavigateAndRefreshCommands(string url, params Command[] commands)
        {
			try
			{
				foreach (var command in commands)
				{
					command.ChangeCanExecute();
				}

				await Shell.Current.GoToAsync(url);


			}
			catch (Exception)
			{

				throw;
			}
        }

		public static async Task Navigate(string url)
		{
            try
            {
                await Shell.Current.GoToAsync(url);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
