using System;
using BlazingPizza.ComponentsLibrary.Map;
using System.Collections.Generic;
using System.Text;

namespace BlazingPizza.Shared
{
    public class OrderWithStatus
    {
        //Simular el tiempo de la preparacion de la orden
        public readonly static TimeSpan PreparationDuration =
            TimeSpan.FromSeconds(10);

        //Simular el tiempo en que se tarda repartidor en entregar 
        //la orden
        public readonly static TimeSpan DeliveryDuration =
            TimeSpan.FromMinutes(1);

        public Order Order { get; set; }
        public string StatusText { get; set; }
        public List<BlazingPizza.ComponentsLibrary.Map.Marker> MapMarkers { get; set; }

        // Obtener el mensaje de estatus de la orden y los marcadores
        // de posición en el mapa.
        public static OrderWithStatus FromOrder(Order order)
        {
            // Para simular un proceso real en el backend,
            // simularemos cambios en el estatus basándonos en
            // el tiempo transcurrido desde que la orden fue realizad
            string Message;
            List<Marker> Markers;
            // Tiempo en que se despacha a entrega el pedido.
            var DispatchTime = order.CreatedTime.Add(PreparationDuration);
            if (DateTime.Now < DispatchTime)
            {
                Message = "Preparando";
                Markers = new List<Marker>
                {
                   ToMapMarker("Usted",order.DeliveryLocation, showPopup: true)
                };
            }
            else if (DateTime.Now < DispatchTime + DeliveryDuration)
            {
                Message = "En Camino";
                //Simular la pocision del repartidorr
                var StartPosition = ComputeStartPosition(order);
                var PropotionOfDeliveryCompleted =
                    Math.Min(1, (DateTime.Now - DispatchTime).TotalMilliseconds
                    / DeliveryDuration.TotalMilliseconds);
                var DrivePosition = LatLong.Interpolate(
                    StartPosition, order.DeliveryLocation,
                    PropotionOfDeliveryCompleted);
                Markers = new List<Marker>
                    {
                        ToMapMarker("Usted", order.DeliveryLocation),
                        ToMapMarker("Repartidor",DrivePosition,showPopup:true),
                    };
            }
            else
            {
                Message = "Entregado";
                Markers = new List<Marker>
                {
                    ToMapMarker("Ubicacion de entrea",order.DeliveryLocation,showPopup: true),
                };
            }

            return new OrderWithStatus
            {
                Order = order,
                StatusText = Message,
                MapMarkers = Markers,
            };

        }

        private static LatLong ComputeStartPosition(Order order)
        {
            //Obtener una Posicion aleatoria
            var Random = new Random(order.OrderId);
            var Distance = 0.01 + Random.NextDouble() * 0.02;
            var Angle = Random.NextDouble() * Math.PI * 2;
            var Offset =
                (Distance * Math.Cos(Angle),
                Distance * Math.Sin(Angle));
            return new LatLong(
                order.DeliveryLocation.Latitude + Offset.Item1, order.DeliveryLocation.Longitude + Offset.Item2);
        }

        //Obtener una Posicion en el mapa,
        static Marker ToMapMarker(string description, LatLong coords, bool showPopup = false)
            => new Marker
            {
                Description = description,
                X = coords.Longitude,
                y = coords.Latitude,
                ShowPopup = showPopup
            };

    }
}
