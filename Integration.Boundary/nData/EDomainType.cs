using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Boundary.nData
{
    public class EDomainType : cBaseConstType<EDomainType>
    {

		/// <summary>
		/// Burdaki tanımlar projenin mickro servis hizmeti verecek parçaların domain adları olacak
		/// Burdaki domainler glabal veritabanınına mikroservis hizmetleri olarak eklenip action lar burdaki domainler üzerinden işleyecek
		/// </summary>
        public static EDomainType BatchJob = new EDomainType(GetVariableName(() => BatchJob), 10);
        public static EDomainType TwitterBot = new EDomainType(GetVariableName(() => TwitterBot), 15);
        public static EDomainType BPA_SingleCore = new EDomainType(GetVariableName(() => BPA_SingleCore), 20);

		/// <summary>
		/// Bunlar farklı domainde run eiyormuş gibi işlesinler diye eklendi
		/// </summary>
		public static EDomainType UpdateDB = new EDomainType(GetVariableName(() => UpdateDB), 30);
		public static EDomainType QueryTester = new EDomainType(GetVariableName(() => QueryTester), 40);


		public static List<EDomainType> TypeList { get; set; }

        public EDomainType(string _Code, int _ID)
            : base(_Code, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EDomainType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EDomainType GetByID(int _ID, EDomainType _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static EDomainType GetByName(string _Name, EDomainType _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
