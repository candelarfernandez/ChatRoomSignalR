# Proyecto de Subastas en Tiempo Real con SignalR

## Descripción

Este proyecto es una aplicación de subastas en tiempo real que permite a los usuarios crear salas de subasta, unirse a ellas y realizar ofertas por objetos sin necesidad de refrescar la página.
Utiliza la librería SignalR para gestionar la comunicación en tiempo real entre el servidor y los clientes.

## Funcionalidades

- **Crear Salas de Subasta**: Los usuarios pueden crear salas para subastar sus objetos.
- **Unirse a Salas**: Otros usuarios pueden unirse a las salas existentes para participar en las subastas.
- **Ofertas en Tiempo Real**: Los usuarios pueden hacer ofertas por los objetos en tiempo real, y todos los participantes de la sala verán las ofertas sin necesidad de recargar la página.
- **Finalizar Subasta**: El usuario que creó la sala puede finalizar la subasta en cualquier momento. Esto guarda la venta y desactiva la sala.
- **Notificaciones en Tiempo Real**: Se notifica en tiempo real a los usuarios cuando otros se unen a la sala, cuando se crean nuevas salas y cuando se realizan nuevas ofertas.
