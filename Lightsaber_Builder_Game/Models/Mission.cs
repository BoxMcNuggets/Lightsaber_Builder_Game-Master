using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightsaber_Builder_Game.Models
{
    public class Mission
    {
        #region ENUMS

        public enum MissionStatus 
        {
            Incomplete,
            Complete
        }

        #endregion

        #region FIELDS

        private int _id;
        private string _name;
        private string _description;
        private MissionStatus _status;
        private string _statusDetail;

        #endregion

        #region PROPERTIES

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public MissionStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string StatusDetail
        {
            get { return _statusDetail; }
            set { _statusDetail = value; }
        }

        #endregion

        public Mission() 
        {

        }
        public Mission(int id, string name, MissionStatus status) 
        {
            _id = id;
            _name = name;
            _status = status;
        }
    }
}
