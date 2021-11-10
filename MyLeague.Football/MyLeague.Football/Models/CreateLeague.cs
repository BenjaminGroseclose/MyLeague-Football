using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeague.Football.Models
{
    public class CreateLeague : ObservableObject
    {
        private string coachFirstName;

        public string CoachFirstName
        {
            get => coachFirstName;
            set => SetProperty(ref coachFirstName, value);
        }

        private string coachLastName;

        public string CoachLastName
        {
            get => coachLastName;   
            set => SetProperty(ref coachLastName, value);
        }

        public int franchiseId;

        public int FranchiseId
        {
            get => franchiseId;
            set => SetProperty(ref franchiseId, value);
        }
    }
}
