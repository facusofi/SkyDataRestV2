using NLog;
using SkyDataRestV2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SkyDataRestV2.Controllers
{
    [Route("api/v2")]
    public class RequestsController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        [Route("api/v2/valuesGet")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("Test eco");
        }
        // POST: api/Products
        [Route("api/v2/values")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]string value)
        {
            Request item = Newtonsoft.Json.JsonConvert.DeserializeObject<Request>(value);
            logger.Info("Datos recibidos -> { JsonConvert.SerializeObject(item) " + value.ToString());
            logger.Info("Resultado -> " + InsertValues(item));
            return Ok("Salio OK");

        }

        private static string InsertValues(Request request)
        {
            try
            {
                string connetionString = null;
                SqlConnection connection;
                SqlDataAdapter adapter;
                SqlCommand command = new SqlCommand();
                SqlParameter param;
                SqlParameter paramReturnErrorCode;
                SqlParameter paramReturnErrorMessage;
                string strResponse;
                string longitud;
                string latitud;



                connetionString = ConfigurationManager.AppSettings["cnn"];
                connection = new SqlConnection(connetionString);

                connection.Open();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_AddGpsHistoricoMovilesActuales";

                param = new SqlParameter("@patente", request.license_plane);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;
                command.Parameters.Add(param);



                if (!request.lon.ToString().Contains(InitSepDecimal()))
                {
                    longitud = request.lon.ToString().Replace(OtroSepDecimal(), InitSepDecimal());
                }
                else
                {
                    longitud = request.lon.ToString();
                }
                if (!request.lat.ToString().Contains(InitSepDecimal()))
                {
                    latitud = request.lat.ToString().Replace(OtroSepDecimal(), InitSepDecimal());
                }
                else
                {
                    latitud = request.lat.ToString();
                }

                param = new SqlParameter("@latitud", Convert.ToDecimal(latitud));
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.Decimal;
                command.Parameters.Add(param);

                param = new SqlParameter("@longitud", Convert.ToDecimal(longitud));
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.Decimal;
                command.Parameters.Add(param);

                param = new SqlParameter("@fecHorTransmision", request.event_time);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.DateTime;
                command.Parameters.Add(param);

                paramReturnErrorCode = new SqlParameter("@errorCode", " ");
                paramReturnErrorCode.Direction = ParameterDirection.Output;
                paramReturnErrorCode.DbType = DbType.String;
                command.Parameters.Add(paramReturnErrorCode);

                paramReturnErrorMessage = new SqlParameter("@errorMessage", SqlDbType.VarChar, 100);
                paramReturnErrorMessage.Direction = ParameterDirection.Output;
                paramReturnErrorMessage.DbType = DbType.String;
                command.Parameters.Add(paramReturnErrorMessage);

                command.ExecuteNonQuery();
                adapter = new SqlDataAdapter(command);

                if (paramReturnErrorCode.Value.ToString() == string.Empty)
                {
                    strResponse = "Se inserto el registro correactamente: " + request.license_plane;
                }
                else
                {
                    strResponse = "Se inserto el registro con error: " + request.license_plane  + " - " + paramReturnErrorMessage.Value.ToString();
                }

                connection.Close();
                return strResponse;


            }
            catch (Exception e)
            {

                logger.Error(e); 
                return e.Message;
            }
        }

        private static string InitSepDecimal()
        {
            string wSepDecimal;

            if (Convert.ToDecimal("2.5") > 3)
                wSepDecimal = ",";
            else
                wSepDecimal = ".";


            return wSepDecimal;

        }

        private static string OtroSepDecimal()
        {
            string wSepDecimal;

            if (Convert.ToDecimal("2.5") > 3)
                wSepDecimal = ".";
            else
                wSepDecimal = ",";


            return wSepDecimal;

        }

    }
}
