using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace SoficCanteen
{
    public class DBUti
    {
        #region Get DB
        public static DataTable GetData(string conn, string chuoi, string ngay, string loaithucdonId,string khunggioanId, string procedureName)
        {
            //DataProvider _getdata = new DataProvider();
            return DataProvider.GetDataTable(conn, procedureName, CommandType.StoredProcedure
                , new SqlParameter("@chuoi", chuoi)
                , new SqlParameter("@ngay", ngay)
                , new SqlParameter("@loaithucdonId", loaithucdonId)
                , new SqlParameter("@khunggioanId", khunggioanId)
                );
        }
        public static DataTable GetInfo(string conn,string chuoi, string procedureName)
        {
            //DataProvider _getdata = new DataProvider();
            return DataProvider.GetDataTable(conn, procedureName, CommandType.StoredProcedure
                , new SqlParameter("@id", chuoi)
                );
        }
        public static DataTable GetKhungGioAn(string conn,string khunggioanId, string procedureName)
        {
            //DataProvider _getdata = new DataProvider();
            return DataProvider.GetDataTable(conn, procedureName, CommandType.StoredProcedure
                 , new SqlParameter("@id", khunggioanId)
                );
        }
        public static DataTable GetConfig(string conn, string khunggioanId, string loaithucdonId, string procedureName)
        {
            //DataProvider _getdata = new DataProvider();
            return DataProvider.GetDataTable(conn, procedureName, CommandType.StoredProcedure
                 , new SqlParameter("@khunggioanId", khunggioanId)
                 , new SqlParameter("@loai", loaithucdonId)
                );
        }
        public static DataTable GetSuatAn(string conn,string suatanId, string procedureName)
        {
            //DataProvider _getdata = new DataProvider();
            return DataProvider.GetDataTable(conn, procedureName, CommandType.StoredProcedure
                 , new SqlParameter("@id", suatanId)
                );
        }
        public static DataTable GetThucDon(string conn, string id, string loai, string ngay, string procedureName)
        {
            //DataProvider _getdata = new DataProvider();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@khunggioanId", id));
            param.Add(new SqlParameter("@loai", loai));
            param.Add(new SqlParameter("@ngay", ngay));
            return DataProvider.GetDataTable(conn, procedureName, CommandType.StoredProcedure
                , param.ToArray()
                );
        }
       
        public static byte[] GetImage(string conn, string maNhanVien, string procedureName)
        {
            return DataProvider.MyExecuteScalar(conn, procedureName, CommandType.StoredProcedure
                , new SqlParameter("@maNhanVien", maNhanVien)
                );
        }
        #endregion

        #region Update DB
        public static bool UpdateConfig(string conn, string apiServer, string locationId, bool remember)
        {
            bool result = false;
            return result = DataProvider.MyExcuteNonQuery(conn, "SP_UpdateConfig", CommandType.StoredProcedure
                , new SqlParameter("@apiServer", apiServer)
                , new SqlParameter("@locationId", locationId)
                , new SqlParameter("@remember", remember)
                  );
        }
        public static bool UpdateOrderNV(string conn, bool isDanhan, string parentId, bool inputMode, string ngay, string updatedBy, string khunggioanId, string nhanvienId)
        {
            bool result = false;
            //DataProvider _updateData = new DataProvider();
            return result = DataProvider.MyExcuteNonQuery(conn, "SP_UpdateOrderNV", CommandType.StoredProcedure
            
                , new SqlParameter("@isDanhan", isDanhan)
                , new SqlParameter("@parentId", parentId)
                , new SqlParameter("@inputMode", inputMode)
                , new SqlParameter("@ngay", ngay)
                , new SqlParameter("@updatedBy", updatedBy)
                , new SqlParameter("@khunggioanId", khunggioanId)
                , new SqlParameter("@nhanvienId", nhanvienId)

                  );
        }
        public static bool UpdateDichVu(string conn, bool isDanhan, string newId, string nhanVien_Id, string parentId, bool inputMode, string monAn_Id, int soLuongDangKy, int soluong, string updatedBy, string locationId, int loaithanhtoan)
        {
            bool result = false;
            //DataProvider _updateData = new DataProvider();
            return result = DataProvider.MyExcuteNonQuery(conn, "SP_UpdateDichVu", CommandType.StoredProcedure
                , new SqlParameter("@isDanhan", isDanhan)
                , new SqlParameter("@nhanVien_Id", nhanVien_Id)
                , new SqlParameter("@parentId", parentId)
                , new SqlParameter("@inputMode", inputMode)
                , new SqlParameter("@monAn_Id", monAn_Id)
                , new SqlParameter("@soLuongDangKy", soLuongDangKy)
                , new SqlParameter("@soluong", soluong)
                , new SqlParameter("@updatedBy", updatedBy)
                , new SqlParameter("@locationId", locationId)
                , new SqlParameter("@loaithanhtoan", loaithanhtoan)
                  );
        }
        public static bool UpdateOrderKH(string conn, string parentId, bool inputMode, string monAn_Id, int soluong, string updatedBy, string loaithucdonId, string khunggioanId)
        {
            bool result = false;
            //DataProvider _updateData = new DataProvider();
            return result = DataProvider.MyExcuteNonQuery(conn, "SP_UpdateOrderKH", CommandType.StoredProcedure
                , new SqlParameter("@parentId", parentId)
                , new SqlParameter("@inputMode", inputMode)
                , new SqlParameter("@monAn_Id", monAn_Id)
                , new SqlParameter("@soluong", soluong)
                , new SqlParameter("@updatedBy", updatedBy)
                , new SqlParameter("@loaithucdonId", loaithucdonId)
                , new SqlParameter("@khunggioanId", khunggioanId)
                  );
        }
        public static bool UpdatePush(string conn, string id, string storeProcedure)
        {
            bool result = false;
            //DataProvider _updateData = new DataProvider();
            return result = DataProvider.MyExcuteNonQuery(conn, storeProcedure, CommandType.StoredProcedure
                , new SqlParameter("@id", id)
                  );
        }
        public static bool UpdatePicture(string conn, string maNhanVien, byte[] imageFile, string storeProcedure)
        {
            bool result = false;
            return result = DataProvider.MyExcuteNonQuery(conn, storeProcedure, CommandType.StoredProcedure
                , new SqlParameter("@maNhanVien", maNhanVien)
                , new SqlParameter("@image", imageFile)
                  );
        }
        #endregion
    }
}
