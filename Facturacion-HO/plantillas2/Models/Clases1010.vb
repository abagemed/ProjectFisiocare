Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Xml
Imports System.Xml.Schema
Public Class Clases

End Class

Public Class Emisor

    Public Rfc As String = String.Empty
    Public Nombre As String = String.Empty
    'Opcional
    Public RegimenFiscal As String = String.Empty
End Class

Public Class Receptor
    Public Rfc As String = String.Empty
    Public Nombre As String = String.Empty
    'Opcional
    Public ResidenciaFiscal As String = String.Empty
    Public DomicilioFiscalReceptor As String = String.Empty
    Public RegimenFiscalReceptor As String = String.Empty
    'Opcional
    Public NumRegIdTrib As String = String.Empty
    'Opcional
    Public UsoCFDI As String = String.Empty
End Class

' AB SAT 4.0
Public Class Concepto
    Public ClaveProdServ As String = String.Empty
    Public NoIdentificacion As String = String.Empty
    'Opcional
    Public Cantidad As Decimal = 0
    Public ClaveUnidad As String = String.Empty
    Public Unidad As String = String.Empty
    'Opcional
    Public Descripcion As String = String.Empty
    Public ValorUnitario As [Decimal] = 0
    Public Importe As [Decimal] = 0
    Public Descuento As [Decimal] = 0
    'Opcional
    Public Impuestos As New ImpuestosC()
    Public InformacionAduanera As New List(Of InformacionAduanera)()
    Public CuentaPredial As New CuentaPredial()
    'public ComplementoConcepto
    Public Parte As New List(Of Parte)()
    Public ObjetoImp As String = String.Empty
End Class

Public Class ImpuestosC
    Public Traslados As New List(Of TrasladoC)()
    Public Retenciones As New List(Of RetencionC)()
End Class

Public Class TrasladoC
    Public Base As Decimal = 0
    Public Impuesto As String = String.Empty
    Public TipoFactor As String = String.Empty
    Public TasaOCuota As Decimal = 0
    Public Importe As Decimal = 0
End Class

Public Class RetencionC
    Public Base As Decimal = 0
    Public Impuesto As String = String.Empty
    Public TipoFactor As String = String.Empty
    Public TasaOCuota As Decimal = 0
    Public Importe As Decimal = 0
End Class

Public Class InformacionAduanera
    Public NumeroPedimento As String = String.Empty
End Class

Public Class CuentaPredial
    Public Numero As String = String.Empty
End Class

Public Class Parte
    Public ClaveProdServ As String = String.Empty
    Public NoIdentificacion As String = String.Empty
    'Opcional
    Public Cantidad As Decimal = 0
    Public Unidad As String = String.Empty
    'Opcional
    Public Descripcion As String = String.Empty
    Public ValorUnitario As Decimal = 0
    Public Importe As Decimal = 0
    Public InformacionAduanera As New List(Of InformacionAduanera)()
    Public ObjetoImp As String = String.Empty
End Class

Public Class Traslado
    Public Base As String = String.Empty
    Public Impuesto As String = String.Empty
    Public TipoFactor As String = String.Empty
    Public TasaOCuota As Decimal = 0
    Public Importe As Decimal = 0
End Class

Public Class Retencion
    Public Impuesto As String = String.Empty
    Public Importe As Decimal = 0
End Class

Public Class Impuestos
    Public TotalImpuestosRetenidos As Decimal = 0
    Public TotalImpuestosTrasladados As Decimal = 0
    Public Retenciones As New List(Of Retencion)()
    Public Traslados As New List(Of Traslado)()
End Class

Public Class Complemento
    Public TimbreFiscalDigital As New TimbreFiscalDigital()
    Public Pagos As New Pagos()
    'Nomina
    Public Nomina As New Nomina()
End Class

Public Class TimbreFiscalDigital
    Public version As String = String.Empty
    Public UUID As String = String.Empty
    Public FechaTimbrado As String = String.Empty
    Public SelloCFD As String = String.Empty
    Public NoCertificadoSAT As String = String.Empty
    Public SelloSAT As String = String.Empty
    Public RfcProvCertif As String = String.Empty
End Class

Public Class CfdiRelacionados
    Public TipoRelacion As String = String.Empty
    Public CfdiRelacionado As New List(Of CfdiRelacionado)()
End Class

Public Class CfdiRelacionado
    Public UUID As String
End Class


Public Class DoctoRelacionado
    Public IdDocumento As String = String.Empty
    Public Serie As String = String.Empty
    'Opcional
    Public Folio As String = String.Empty
    'Opcional
    Public MonedaDR As String = String.Empty
    Public TipoCambioDR As String = String.Empty
    'Opcional
    Public MetodoDePagoDR As String = String.Empty
    Public NumParcialidad As String = String.Empty
    'Opcional
    Public ImpSaldoAnt As Decimal = 0
    'Opcional
    Public ImpPagado As Decimal = 0
    'Opcional
    Public ImpSaldoInsoluto As Decimal = 0
    'Opcional
End Class

Public Class Pago
    Public FechaPago As String = String.Empty
    Public FormaDePagoP As String = String.Empty
    Public MonedaP As String = String.Empty
    Public TipoCambioP As Decimal = 0
    'opcional
    Public Monto As Decimal = 0
    Public NumOperacion As String = String.Empty
    'opcional
    Public RfcEmisorCtaOrd As String = String.Empty
    'opcional
    Public NomBancoOrdExt As String = String.Empty
    'opcional
    Public CtaOrdenante As String = String.Empty
    'opcional
    Public RfcEmisorCtaBen As String = String.Empty
    'opcional
    Public CtaBeneficiario As String = String.Empty
    'opcional
    Public TipoCadPago As String = String.Empty
    'opcional
    Public CertPago As String = String.Empty
    'opcional
    Public CadPago As String = String.Empty
    'opcional
    Public SelloPago As String = String.Empty
    'opcional
    Public DoctoRelacionado As New List(Of DoctoRelacionado)()
    Public Impuestos As New List(Of Impuestos)()
    Friend Montost As String
    Friend Montoiv As String

End Class

Public Class Pagos
    Public Version As String = String.Empty
    Public pagos As New List(Of Pago)()
End Class

'AB SAT 4.0
Public Class Comprobante
    Public Version As String = "4.0"
    Public Serie As String = String.Empty
    Public Folio As String = String.Empty
    Public Fecha As String = String.Empty
    'Fecha y hora de expedicion del comprobante
    Public Sello As String = String.Empty
    Public FormaPago As String = String.Empty
    'Opcional
    Public NoCertificado As String = String.Empty
    Public Certificado As String = String.Empty
    Public CondicionesDePago As String = String.Empty
    Public Subtotal As Decimal = 0
    Public Descuento As Decimal = 0
    Public Moneda As String = String.Empty
    Public TipoCambio As Decimal = 0
    Public Total As Decimal = 0
    Public Exportacion As String = String.Empty
    Public TipoDeComprobante As String = String.Empty
    Public MetodoPago As String = String.Empty
    'Opcional
    Public LugarExpedicion As String = String.Empty
    Public Confirmacion As String = String.Empty
    'Opcional
    Public CfdiRelacionados As New CfdiRelacionados()
    'Opcional
    Public Emisor As New Emisor()
    Public Receptor As New Receptor()
    Public Conceptos As New List(Of Concepto)()
    Public Impuestos As New Impuestos()
    'ADENDA

    Public Complemento As New Complemento()


    Public TotalLetra As String = String.Empty


End Class


#Region "Clases Nomina"

''' <summary>
''' Nodo opcional para que las entidades adheridas al Sistema Nacional de Coordinación Fiscal realcen la identificacion del origen de los recursos.
''' Secuencia(min, max)
''' Secuencia (0,1)
''' </summary>
Public Class NEntidadSNCF
    Public OrigenRecurso As String = String.Empty
    Public MontoRecursoPropio As Single = 0.0F
End Class

Public Class NSubContratacion
    ''' <summary>
    ''' Rfc de la person que lo subcontrata
    ''' </summary>
    Public RfcLabora As String = String.Empty
    Public PorcentajeTiempo As String = String.Empty
End Class

Public Class NEmisor
    Public Curp As String = String.Empty
    Public RegistroPatronal As String = String.Empty
    Public RfcPatronOrigen As String = String.Empty
    Public EntidadSNCF As New NEntidadSNCF()
End Class

Public Class NReceptor
    Public Curp As String = String.Empty
    Public NumSeguridadSocial As String = String.Empty
    'Opcional
    Public FechaInicioRelLaboral As String = String.Empty
    'Opcional
    Public Antiguedad As String = String.Empty
    'Opcional
    Public TipoContrato As String = String.Empty
    Public Sindicalizado As String = String.Empty
    'Opcional
    Public TipoJornada As String = String.Empty
    'Opcional
    Public TipoRegimen As String = String.Empty
    Public NumEmpleado As String = String.Empty
    Public Departamento As String = String.Empty
    'Opcional
    Public Puesto As String = String.Empty
    'Opcional
    Public RiesgoPuesto As String = String.Empty
    'Opcional
    Public PeriodicidadPago As String = String.Empty
    Public Banco As String = String.Empty
    'Opcional
    Public CuentaBancaria As String = String.Empty
    'Opcional
    Public SalarioBaseCotApor As Single = 0.0F
    'Opcional
    Public SalarioDiarioIntegrado As Single = 0.0F
    'Opcional
    Public ClaveEntFed As String = String.Empty
    Public SubContratacion As New List(Of NSubContratacion)()
    'Opcional
End Class

Public Class NDeduccion
    Public TipoDeduccion As String = String.Empty
    Public Clave As String = String.Empty
    Public Concepto As String = String.Empty
    Public Importe As Single = 0.0F
End Class

Public Class NDeducciones
    Public TotalOtrasDeducciones As Single
    'Opcional
    Public TotalImpuestosRetenidos As Single
    'Opcional
    Public Deducciones As New List(Of NDeduccion)()
End Class

Public Class NJubilacionPensionRetiro
    Public TotalUnaExhibicion As Single = 0.0F
    'Opcional
    Public TotalParcialidad As Single = 0.0F
    'Opcional
    Public MontoDiario As Single = 0.0F
    'Opcional
    Public IngresoAcumulable As Single = 0.0F
    Public IngresoNoAcumulable As Single = 0.0F
End Class

Public Class NSeparacionIndeminzacion
    Public TotalPagado As Single = 0.0F
    Public NumAnosServicio As Single = 0.0F
    Public UltimoSueldoMensOrd As Single = 0.0F
    Public IngresoAcumulable As Single = 0.0F
    Public IngresoNoAcumulable As Single = 0.0F
End Class

Public Class NAccionesOTitulos
    Public ValorMercado As String = String.Empty
    Public PrecioAlOtorgarse As String = String.Empty
End Class

Public Class HorasExtra
    Public Dias As String = String.Empty
    Public TipoHoras As String = String.Empty
    Public HorasExtraa As String = String.Empty
    Public ImportePagado As Single = 0.0F
End Class

Public Class NPercepcion
    Public TipoPercepcion As String = String.Empty
    Public Clave As String = String.Empty
    Public Concepto As String = String.Empty
    Public ImporteGravado As Single = 0.0F
    Public ImporteExento As Single = 0.0F
    Public AccionesOTitulos As New NAccionesOTitulos()
    'Opcional
    Public HorasExtras As New List(Of HorasExtra)()
    'Opcional
End Class

Public Class NPercepciones
    Public TotalSueldos As Single = 0.0F
    'Opcional
    Public TotalSeparacionIndemnizacion As Single = 0.0F
    'Opcional
    Public TotalJubilacionPensionRetiro As Single = 0.0F
    'Opcional
    Public TotalGravado As Single
    Public TotalExento As Single
    ''' <summary>
    ''' Nodo condicional para expresar la informacion detallada de pagos por jubilacion, pensiones o haberes de retiro
    ''' Percepcion(minimon, maximo)
    ''' Percepcion(1, Ilimitado)
    ''' </summary>
    Public Percepciones As New List(Of NPercepcion)()
    ''' <summary>
    ''' JubilacionPensionRetiro(minimo, maximo)
    ''' JubilacionPensionRetiro(0,1)
    ''' </summary>
    Public JubilacionPensionRetiro As New NJubilacionPensionRetiro()
    ''' <summary>
    ''' SeparacionIndeminzacion(minimo, maximo)
    '''  SeparacionIndeminzacion(0, 1)
    ''' </summary>
    Public SeparacionIndeminzacion As New NSeparacionIndeminzacion()

End Class

Public Class NSubsidioAlEmpleo
    Public SubsidioCausado As Single = 0.0F
End Class

Public Class NCompensacionSaldosAFavor
    Public SaldoAFavor As Single = 0.0F
    Public ano As String = String.Empty
    Public RemanenteSalFav As Single = 0.0F
End Class

Public Class NOtroPago
    Public TipoOtroPago As String = String.Empty
    Public Clave As String = String.Empty
    Public Concepto As String = String.Empty
    Public Importe As Single = 0.0F

    ''' <summary>
    ''' SubsidioAlEmpleo (0, 1)
    ''' </summary>
    Public SubsidioAlEmpleo As New NSubsidioAlEmpleo()
    ''' <summary>
    ''' CompensacionSaldosAFavor (0, 1)
    ''' </summary>
    Public CompensacionSaldosAFavor As New NCompensacionSaldosAFavor()
End Class

Public Class Nomina
    Public Version As String = String.Empty
    Public TipoNomina As String = String.Empty
    Public FechaPago As String = String.Empty
    Public FechaFinalPago As String = String.Empty
    Public FechaInicialPago As String = String.Empty
    Public NumDiasPagados As String = String.Empty
    Public TotalPercepciones As Single = 0.0F
    Public TotalDeducciones As Single = 0.0F
    Public TotalOtrosPagos As Single = 0.0F

    Public Emisor As New NEmisor()
    Public Receptor As New NReceptor()
    Public Percepciones As New NPercepciones()
    Public Deducciones As New NDeducciones()
    Public OtrosPagos As New List(Of NOtroPago)()
End Class

#End Region
