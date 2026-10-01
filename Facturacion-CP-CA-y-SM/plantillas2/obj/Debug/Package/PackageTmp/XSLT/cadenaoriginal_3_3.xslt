<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions" xmlns:cfdi="http://www.sat.gob.mx/cfd/3" xmlns:cce11="http://www.sat.gob.mx/ComercioExterior11" xmlns:donat="http://www.sat.gob.mx/donat" xmlns:divisas="http://www.sat.gob.mx/divisas" xmlns:implocal="http://www.sat.gob.mx/implocal" xmlns:leyendasFisc="http://www.sat.gob.mx/leyendasFiscales" xmlns:pfic="http://www.sat.gob.mx/pfic" xmlns:tpe="http://www.sat.gob.mx/TuristaPasajeroExtranjero" xmlns:nomina12="http://www.sat.gob.mx/nomina12" xmlns:registrofiscal="http://www.sat.gob.mx/registrofiscal" xmlns:pagoenespecie="http://www.sat.gob.mx/pagoenespecie" xmlns:aerolineas="http://www.sat.gob.mx/aerolineas" xmlns:valesdedespensa="http://www.sat.gob.mx/valesdedespensa" xmlns:consumodecombustibles="http://www.sat.gob.mx/consumodecombustibles" xmlns:notariospublicos="http://www.sat.gob.mx/notariospublicos" xmlns:vehiculousado="http://www.sat.gob.mx/vehiculousado" xmlns:servicioparcial="http://www.sat.gob.mx/servicioparcialconstruccion" xmlns:decreto="http://www.sat.gob.mx/renovacionysustitucionvehiculos" xmlns:destruccion="http://www.sat.gob.mx/certificadodestruccion" xmlns:obrasarte="http://www.sat.gob.mx/arteantiguedades" xmlns:ine="http://www.sat.gob.mx/ine" xmlns:iedu="http://www.sat.gob.mx/iedu" xmlns:ventavehiculos="http://www.sat.gob.mx/ventavehiculos" xmlns:terceros="http://www.sat.gob.mx/terceros" xmlns:pago10="http://www.sat.gob.mx/Pagos" xmlns:ecc11="http://www.sat.gob.mx/EstadoDeCuentaCombustible" xmlns:detallista="http://www.sat.gob.mx/detallista" xmlns:ecc12="http://www.sat.gob.mx/EstadoDeCuentaCombustible12" xmlns:consumodecombustibles11="http://www.sat.gob.mx/ConsumoDeCombustibles11">
	<xsl:output method="text" version="1.0" encoding="UTF-8" indent="no" />
	<xsl:template name="Requerido" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:param name="valor" />|<xsl:call-template name="ManejaEspacios"><xsl:with-param name="s" select="$valor" /></xsl:call-template></xsl:template>
	<xsl:template name="Opcional" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:param name="valor" />
		<xsl:if test="$valor">|<xsl:call-template name="ManejaEspacios"><xsl:with-param name="s" select="$valor" /></xsl:call-template></xsl:if>
	</xsl:template>
	<xsl:template name="ManejaEspacios" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:param name="s" />
		<xsl:value-of select="normalize-space(string($s))" />
	</xsl:template>
	<xsl:template match="ecc11:EstadoDeCuentaCombustible" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoOperacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumeroDeCuenta" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@SubTotal" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Total" />
		</xsl:call-template>
		<xsl:apply-templates select="./ecc11:Conceptos" />
	</xsl:template>
	<xsl:template match="ecc11:Conceptos" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./ecc11:ConceptoEstadoDeCuentaCombustible">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="ecc11:Traslados" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./ecc11:Traslado">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="ecc11:ConceptoEstadoDeCuentaCombustible" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Identificador" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Fecha" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Rfc" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ClaveEstacion" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TAR" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Cantidad" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NoIdentificacion" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Unidad" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NombreCombustible" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@FolioOperacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ValorUnitario" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Importe" />
		</xsl:call-template>
		<xsl:apply-templates select="./ecc11:Traslados" />
	</xsl:template>
	<xsl:template match="ecc11:Traslado" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Impuesto" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TasaoCuota" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Importe" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="donat:Donatarias" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@noAutorizacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@fechaAutorizacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@leyenda" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="divisas:Divisas" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@tipoOperacion" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="implocal:ImpuestosLocales" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TotaldeRetenciones" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TotaldeTraslados" />
		</xsl:call-template>
		<xsl:for-each select="implocal:RetencionesLocales">
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@ImpLocRetenido" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@TasadeRetencion" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@Importe" />
			</xsl:call-template>
		</xsl:for-each>
		<xsl:for-each select="implocal:TrasladosLocales">
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@ImpLocTrasladado" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@TasadeTraslado" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@Importe" />
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="leyendasFisc:LeyendasFiscales" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@version" />
		</xsl:call-template>
		<xsl:for-each select="./leyendasFisc:Leyenda">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="leyendasFisc:Leyenda" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@disposicionFiscal" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@norma" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@textoLeyenda" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="pfic:PFintegranteCoordinado" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ClaveVehicular" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Placa" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@RFCPF" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="tpe:TuristaPasajeroExtranjero" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@fechadeTransito" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@tipoTransito" />
		</xsl:call-template>
		<xsl:apply-templates select="./tpe:datosTransito" />
	</xsl:template>
	<xsl:template match="tpe:datosTransito" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Via" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoId" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumeroId" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Nacionalidad" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@EmpresaTransporte" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@IdTransporte" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="nomina12:Nomina" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoNomina" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@FechaPago" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@FechaInicialPago" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@FechaFinalPago" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumDiasPagados" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalPercepciones" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalDeducciones" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalOtrosPagos" />
		</xsl:call-template>
		<xsl:for-each select="./nomina12:Emisor">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:for-each select="./nomina12:Receptor">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:for-each select="./nomina12:Percepciones">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:for-each select="./nomina12:Deducciones">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:for-each select="./nomina12:OtrosPagos">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:for-each select="./nomina12:Incapacidades">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="nomina12:Emisor" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Curp" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@RegistroPatronal" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@RfcPatronOrigen" />
		</xsl:call-template>
		<xsl:for-each select="./nomina12:EntidadSNCF">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="nomina12:EntidadSNCF" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@OrigenRecurso" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@MontoRecursoPropio" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="nomina12:Receptor" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Curp" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumSeguridadSocial" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@FechaInicioRelLaboral" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Antigüedad" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoContrato" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Sindicalizado" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TipoJornada" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoRegimen" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumEmpleado" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Departamento" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Puesto" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@RiesgoPuesto" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@PeriodicidadPago" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Banco" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@CuentaBancaria" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@SalarioBaseCotApor" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@SalarioDiarioIntegrado" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ClaveEntFed" />
		</xsl:call-template>
		<xsl:for-each select="./nomina12:SubContratacion">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="nomina12:SubContratacion" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@RfcLabora" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@PorcentajeTiempo" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="nomina12:Percepciones" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalSueldos" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalSeparacionIndemnizacion" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalJubilacionPensionRetiro" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TotalGravado" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TotalExento" />
		</xsl:call-template>
		<xsl:for-each select="./nomina12:Percepcion">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:for-each select="./nomina12:JubilacionPensionRetiro">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:for-each select="./nomina12:SeparacionIndemnizacion">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="nomina12:Percepcion" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoPercepcion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Clave" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Concepto" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ImporteGravado" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ImporteExento" />
		</xsl:call-template>
		<xsl:for-each select="./nomina12:AccionesOTitulos">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:for-each select="./nomina12:HorasExtra">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="nomina12:AccionesOTitulos" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ValorMercado" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@PrecioAlOtorgarse" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="nomina12:HorasExtra" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Dias" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoHoras" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@HorasExtra" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ImportePagado" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="nomina12:JubilacionPensionRetiro" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalUnaExhibicion" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalParcialidad" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@MontoDiario" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@IngresoAcumulable" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@IngresoNoAcumulable" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="nomina12:SeparacionIndemnizacion" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TotalPagado" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumAñosServicio" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@UltimoSueldoMensOrd" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@IngresoAcumulable" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@IngresoNoAcumulable" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="nomina12:Deducciones" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalOtrasDeducciones" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalImpuestosRetenidos" />
		</xsl:call-template>
		<xsl:for-each select="./nomina12:Deduccion">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="nomina12:Deduccion" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoDeduccion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Clave" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Concepto" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Importe" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="nomina12:OtrosPagos" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./nomina12:OtroPago">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="nomina12:OtroPago" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoOtroPago" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Clave" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Concepto" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Importe" />
		</xsl:call-template>
		<xsl:for-each select="./nomina12:SubsidioAlEmpleo">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:for-each select="./nomina12:CompensacionSaldosAFavor">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="nomina12:SubsidioAlEmpleo" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@SubsidioCausado" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="nomina12:CompensacionSaldosAFavor" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@SaldoAFavor" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Año" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@RemanenteSalFav" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="nomina12:Incapacidades" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./nomina12:Incapacidad">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="nomina12:Incapacidad" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@DiasIncapacidad" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoIncapacidad" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ImporteMonetario" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="registrofiscal:CFDIRegistroFiscal" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Folio" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="pagoenespecie:PagoEnEspecie" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@CvePIC" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@FolioSolDon" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@PzaArtNombre" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@PzaArtTecn" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@PzaArtAProd" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@PzaArtDim" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="aerolineas:Aerolineas" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TUA" />
		</xsl:call-template>
		<xsl:apply-templates select="./aerolineas:OtrosCargos" />
	</xsl:template>
	<xsl:template match="aerolineas:OtrosCargos" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TotalCargos" />
		</xsl:call-template>
		<xsl:for-each select="./aerolineas:Cargo">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="aerolineas:Cargo" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@CodigoCargo" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Importe" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="valesdedespensa:ValesDeDespensa" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@tipoOperacion" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@registroPatronal" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@numeroDeCuenta" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@total" />
		</xsl:call-template>
		<xsl:apply-templates select="./valesdedespensa:Conceptos" />
	</xsl:template>
	<xsl:template match="valesdedespensa:Conceptos" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./valesdedespensa:Concepto">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="valesdedespensa:Concepto" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@identificador" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@fecha" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@rfc" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@curp" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@nombre" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@numSeguridadSocial" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@importe" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="consumodecombustibles:ConsumoDeCombustibles" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@tipoOperacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@numeroDeCuenta" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@subTotal" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@total" />
		</xsl:call-template>
		<xsl:apply-templates select="./consumodecombustibles:Conceptos" />
	</xsl:template>
	<xsl:template match="consumodecombustibles:Conceptos" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./consumodecombustibles:ConceptoConsumoDeCombustibles">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="consumodecombustibles:ConceptoConsumoDeCombustibles" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@identificador" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@fecha" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@rfc" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@claveEstacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@cantidad" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@nombreCombustible" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@folioOperacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@valorUnitario" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@importe" />
		</xsl:call-template>
		<xsl:for-each select="./consumodecombustibles:Determinados">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="consumodecombustibles:Determinados" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./consumodecombustibles:Determinado">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="consumodecombustibles:Determinado" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@impuesto" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@tasa" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@importe" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="notariospublicos:NotariosPublicos" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:apply-templates select="./notariospublicos:DescInmuebles" />
		<xsl:apply-templates select="./notariospublicos:DatosOperacion" />
		<xsl:apply-templates select="./notariospublicos:DatosNotario" />
		<xsl:apply-templates select="./notariospublicos:DatosEnajenante" />
		<xsl:apply-templates select="./notariospublicos:DatosAdquiriente" />
	</xsl:template>
	<xsl:template match="notariospublicos:DescInmuebles" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./notariospublicos:DescInmueble">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="notariospublicos:DescInmueble" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoInmueble" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Calle" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NoExterior" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NoInterior" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Colonia" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Localidad" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Referencia" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Municipio" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Estado" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Pais" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@CodigoPostal" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="notariospublicos:DatosOperacion" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumInstrumentoNotarial" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@FechaInstNotarial" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@MontoOperacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Subtotal" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@IVA" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="notariospublicos:DatosNotario" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@CURP" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumNotaria" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@EntidadFederativa" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Adscripcion" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="notariospublicos:DatosEnajenante" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@CoproSocConyugalE" />
		</xsl:call-template>
		<xsl:if test="./notariospublicos:DatosUnEnajenante">
			<xsl:apply-templates select="./notariospublicos:DatosUnEnajenante" />
		</xsl:if>
		<xsl:if test="./notariospublicos:DatosEnajenantesCopSC">
			<xsl:apply-templates select="./notariospublicos:DatosEnajenantesCopSC" />
		</xsl:if>
	</xsl:template>
	<xsl:template match="notariospublicos:DatosUnEnajenante" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Nombre" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ApellidoPaterno" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ApellidoMaterno" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@RFC" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@CURP" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="notariospublicos:DatosEnajenantesCopSC" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./notariospublicos:DatosEnajenanteCopSC">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="notariospublicos:DatosEnajenanteCopSC" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Nombre" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ApellidoPaterno" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ApellidoMaterno" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@RFC" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@CURP" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Porcentaje" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="notariospublicos:DatosAdquiriente" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@CoproSocConyugalE" />
		</xsl:call-template>
		<xsl:if test="./notariospublicos:DatosUnAdquiriente">
			<xsl:apply-templates select="./notariospublicos:DatosUnAdquiriente" />
		</xsl:if>
		<xsl:if test="./notariospublicos:DatosAdquirientesCopSC">
			<xsl:apply-templates select="./notariospublicos:DatosAdquirientesCopSC" />
		</xsl:if>
	</xsl:template>
	<xsl:template match="notariospublicos:DatosUnAdquiriente" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Nombre" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ApellidoPaterno" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ApellidoMaterno" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@RFC" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@CURP" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="notariospublicos:DatosAdquirientesCopSC" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./notariospublicos:DatosAdquirienteCopSC">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="notariospublicos:DatosAdquirienteCopSC" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Nombre" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ApellidoPaterno" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ApellidoMaterno" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@RFC" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@CURP" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Porcentaje" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="/" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">|<xsl:apply-templates select="/vehiculousado:VehiculoUsado" />||</xsl:template>
	<xsl:template match="vehiculousado:VehiculoUsado" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@montoAdquisicion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@montoEnajenacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@claveVehicular" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@marca" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@tipo" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@modelo" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@numeroMotor" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@numeroSerie" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NIV" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@valor" />
		</xsl:call-template>
		<xsl:apply-templates select="./vehiculousado:InformacionAduanera" />
	</xsl:template>
	<xsl:template match="vehiculousado:InformacionAduanera" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@numero" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@fecha" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@aduana" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="servicioparcial:parcialesconstruccion" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumPerLicoAut" />
		</xsl:call-template>
		<xsl:apply-templates select="./servicioparcial:Inmueble" />
	</xsl:template>
	<xsl:template match="servicioparcial:Inmueble" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Calle" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NoExterior" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NoInterior" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Colonia" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Localidad" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Referencia" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Municipio" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Estado" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@CodigoPostal" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="decreto:renovacionysustitucionvehiculos" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoDeDecreto" />
		</xsl:call-template>
		<xsl:apply-templates select="./decreto:DecretoRenovVehicular" />
		<xsl:apply-templates select="./decreto:DecretoSustitVehicular" />
	</xsl:template>
	<xsl:template match="decreto:DecretoRenovVehicular" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@VehEnaj" />
		</xsl:call-template>
		<xsl:for-each select="./decreto:VehiculosUsadosEnajenadoPermAlFab">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:apply-templates select="./decreto:VehiculoNuvoSemEnajenadoFabAlPerm" />
	</xsl:template>
	<xsl:template match="decreto:DecretoSustitVehicular" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@VehEnaj" />
		</xsl:call-template>
		<xsl:apply-templates select="./decreto:VehiculoUsadoEnajenadoPermAlFab" />
		<xsl:apply-templates select="./decreto:VehiculoNuvoSemEnajenadoFabAlPerm" />
	</xsl:template>
	<xsl:template match="decreto:VehiculosUsadosEnajenadoPermAlFab" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@PrecioVehUsado" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoVeh" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Marca" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipooClase" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Año" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Modelo" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NIV" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumSerie" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumPlacas" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumMotor" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumFolTarjCir" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumPedIm" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Aduana" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@FechaRegulVeh" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Foliofiscal" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="decreto:VehiculoNuvoSemEnajenadoFabAlPerm" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Año" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Modelo" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumPlacas" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@RFC" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="decreto:VehiculoUsadoEnajenadoPermAlFab" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@PrecioVehUsado" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoVeh" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Marca" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipooClase" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Año" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Modelo" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NIV" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumSerie" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumPlacas" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumMotor" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumFolTarjCir" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumFolAvisoint" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumPedIm" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Aduana" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@FechaRegulVeh" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Foliofiscal" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="destruccion:certificadodedestruccion" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Serie" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumFolDesVeh" />
		</xsl:call-template>
		<xsl:apply-templates select="./destruccion:VehiculoDestruido" />
		<xsl:apply-templates select="./destruccion:InformacionAduanera" />
	</xsl:template>
	<xsl:template match="destruccion:VehiculoDestruido" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Marca" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipooClase" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Año" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Modelo" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NIV" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumSerie" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumPlacas" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumMotor" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumFolTarjCir" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="destruccion:InformacionAduanera" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumPedImp" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Fecha" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Aduana" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="obrasarte:obrasarteantiguedades" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoBien" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@OtrosTipoBien" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TituloAdquirido" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@OtrosTituloAdquirido" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Subtotal" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@IVA" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@FechaAdquisicion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@CaracterísticasDeObraoPieza" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="cce11:ComercioExterior" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@MotivoTraslado" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoOperacion" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ClaveDePedimento" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@CertificadoOrigen" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumCertificadoOrigen" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumeroExportadorConfiable" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Incoterm" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Subdivision" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Observaciones" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TipoCambioUSD" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalUSD" />
		</xsl:call-template>
		<xsl:apply-templates select="./cce11:Emisor" />
		<xsl:for-each select="./cce11:Propietario">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:apply-templates select="./cce11:Receptor" />
		<xsl:for-each select="./cce11:Destinatario">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:apply-templates select="./cce11:Mercancias" />
	</xsl:template>
	<xsl:template match="cce11:Emisor" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Curp" />
		</xsl:call-template>
		<xsl:apply-templates select="./cce11:Domicilio" />
	</xsl:template>
	<xsl:template match="cce11:Propietario" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumRegIdTrib" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ResidenciaFiscal" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="cce11:Receptor" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumRegIdTrib" />
		</xsl:call-template>
		<xsl:apply-templates select="./cce11:Domicilio" />
	</xsl:template>
	<xsl:template match="cce11:Destinatario" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumRegIdTrib" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Nombre" />
		</xsl:call-template>
		<xsl:for-each select="./cce11:Domicilio">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="cce11:Mercancias" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./cce11:Mercancia">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="cce11:Domicilio" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Calle" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumeroExterior" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumeroInterior" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Colonia" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Localidad" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Referencia" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Municipio" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Estado" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Pais" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@CodigoPostal" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="cce11:Mercancia" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NoIdentificacion" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@FraccionArancelaria" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@CantidadAduana" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@UnidadAduana" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ValorUnitarioAduana" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ValorDolares" />
		</xsl:call-template>
		<xsl:for-each select="./cce11:DescripcionesEspecificas">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="cce11:DescripcionesEspecificas" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Marca" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Modelo" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@SubModelo" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumeroSerie" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="ine:INE" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoProceso" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TipoComite" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@IdContabilidad" />
		</xsl:call-template>
		<xsl:for-each select="./ine:Entidad">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="ine:Entidad" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ClaveEntidad" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Ambito" />
		</xsl:call-template>
		<xsl:for-each select="./ine:Contabilidad">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="ine:Contabilidad" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@IdContabilidad" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="iedu:instEducativas" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@nombreAlumno" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@CURP" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@nivelEducativo" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@autRVOE" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@rfcPago" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="ventavehiculos:VentaVehiculos" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ClaveVehicular" />
		</xsl:call-template>
		<xsl:if test="./@version='1.1'">
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@Niv" />
			</xsl:call-template>
		</xsl:if>
		<xsl:for-each select=".//ventavehiculos:InformacionAduanera">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="ventavehiculos:InformacionAduanera" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@numero" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@fecha" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@aduana" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="terceros:PorCuentadeTerceros" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@rfc" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@nombre" />
		</xsl:call-template>
		<xsl:apply-templates select=".//terceros:InformacionFiscalTercero" />
		<xsl:for-each select=".//terceros:InformacionAduanera">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:if test="./terceros:CuentaPredial">
			<xsl:apply-templates select="./terceros:CuentaPredial" />
		</xsl:if>
		<xsl:for-each select=".//terceros:Retenciones/terceros:Retencion">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:for-each select=".//terceros:Traslados/terceros:Traslado">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="terceros:Retencion" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@impuesto" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@importe" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="terceros:Traslado" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@impuesto" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@tasa" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@importe" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="terceros:InformacionAduanera" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@numero" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@fecha" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@aduana" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="terceros:CuentaPredial" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@numero" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="terceros:InformacionFiscalTercero" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@calle" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@noExterior" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@noInterior" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@colonia" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@localidad" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@referencia" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@municipio" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@estado" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@pais" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@codigoPostal" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="pago10:Pagos" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:for-each select="./pago10:Pago">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="pago10:Pago" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@FechaPago" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@FormaDePagoP" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@MonedaP" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TipoCambioP" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Monto" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumOperacion" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@RfcEmisorCtaOrd" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NomBancoOrdExt" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@CtaOrdenante" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@RfcEmisorCtaBen" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@CtaBeneficiario" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TipoCadPago" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@CertPago" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@CadPago" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@SelloPago" />
		</xsl:call-template>
		<xsl:for-each select="./pago10:DoctoRelacionado">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:for-each select="./pago10:Impuestos">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="pago10:DoctoRelacionado" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@IdDocumento" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Serie" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Folio" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@MonedaDR" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TipoCambioDR" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@MetodoDePagoDR" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumParcialidad" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ImpSaldoAnt" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ImpPagado" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ImpSaldoInsoluto" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="pago10:Impuestos" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalImpuestosRetenidos" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalImpuestosTrasladados" />
		</xsl:call-template>
		<xsl:apply-templates select="./pago10:Retenciones" />
		<xsl:apply-templates select="./pago10:Traslados" />
	</xsl:template>
	<xsl:template match="pago10:Retenciones" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./pago10:Retencion">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="pago10:Traslados" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./pago10:Traslado">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="pago10:Retencion" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Impuesto" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Importe" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="pago10:Traslado" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Impuesto" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoFactor" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TasaOCuota" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Importe" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="detallista:detallista" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@documentStructureVersion" />
		</xsl:call-template>
		<xsl:for-each select="detallista:orderIdentification/detallista:referenceIdentification">
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="." />
			</xsl:call-template>
		</xsl:for-each>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="detallista:orderIdentification/detallista:ReferenceDate" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="detallista:buyer/detallista:gln" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="detallista:seller/detallista:gln" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="detallista:seller/detallista:alternatePartyIdentification" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="detallista:totalAmount/detallista:Amount" />
		</xsl:call-template>
		<xsl:for-each select="detallista:TotalAllowanceCharge/detallista:specialServicesType">
			<xsl:call-template name="Opcional">
				<xsl:with-param name="valor" select="." />
			</xsl:call-template>
		</xsl:for-each>
		<xsl:for-each select="detallista:TotalAllowanceCharge/detallista:Amount">
			<xsl:call-template name="Opcional">
				<xsl:with-param name="valor" select="." />
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="ecc12:EstadoDeCuentaCombustible" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoOperacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumeroDeCuenta" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@SubTotal" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Total" />
		</xsl:call-template>
		<xsl:apply-templates select="./ecc12:Conceptos" />
	</xsl:template>
	<xsl:template match="ecc12:Conceptos" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./ecc12:ConceptoEstadoDeCuentaCombustible">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="ecc12:Traslados" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./ecc12:Traslado">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="ecc12:ConceptoEstadoDeCuentaCombustible" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Identificador" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Fecha" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Rfc" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ClaveEstacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Cantidad" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoCombustible" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Unidad" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NombreCombustible" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@FolioOperacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ValorUnitario" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Importe" />
		</xsl:call-template>
		<xsl:apply-templates select="./ecc12:Traslados" />
	</xsl:template>
	<xsl:template match="ecc12:Traslado" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Impuesto" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TasaOCuota" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Importe" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="consumodecombustibles11:ConsumoDeCombustibles" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@version" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@tipoOperacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@numeroDeCuenta" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@subTotal" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@total" />
		</xsl:call-template>
		<xsl:apply-templates select="./consumodecombustibles11:Conceptos" />
	</xsl:template>
	<xsl:template match="consumodecombustibles11:Conceptos" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./consumodecombustibles11:ConceptoConsumoDeCombustibles">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="consumodecombustibles11:ConceptoConsumoDeCombustibles" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@identificador" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@fecha" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@rfc" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@claveEstacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@tipoCombustible" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@cantidad" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@nombreCombustible" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@folioOperacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@valorUnitario" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@importe" />
		</xsl:call-template>
		<xsl:apply-templates select="./consumodecombustibles11:Determinados" />
	</xsl:template>
	<xsl:template match="consumodecombustibles11:Determinados" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:for-each select="./consumodecombustibles11:Determinado">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="consumodecombustibles11:Determinado" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@impuesto" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@tasaOCuota" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@importe" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="/">|<xsl:apply-templates select="/cfdi:Comprobante" />||</xsl:template>
	<xsl:template match="cfdi:Comprobante">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Version" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Serie" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Folio" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Fecha" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@FormaPago" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NoCertificado" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@CondicionesDePago" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@SubTotal" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Descuento" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Moneda" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TipoCambio" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Total" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoDeComprobante" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@MetodoPago" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@LugarExpedicion" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Confirmacion" />
		</xsl:call-template>
		<xsl:apply-templates select="./cfdi:CfdiRelacionados" />
		<xsl:apply-templates select="./cfdi:Emisor" />
		<xsl:apply-templates select="./cfdi:Receptor" />
		<xsl:apply-templates select="./cfdi:Conceptos" />
		<xsl:apply-templates select="./cfdi:Impuestos" />
		<xsl:for-each select="./cfdi:Complemento">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="cfdi:CfdiRelacionados">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@TipoRelacion" />
		</xsl:call-template>
		<xsl:for-each select="./cfdi:CfdiRelacionado">
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@UUID" />
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="cfdi:Emisor">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Rfc" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Nombre" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@RegimenFiscal" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="cfdi:Receptor">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Rfc" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Nombre" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ResidenciaFiscal" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NumRegIdTrib" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@UsoCFDI" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="cfdi:Conceptos">
		<xsl:for-each select="./cfdi:Concepto">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="cfdi:Concepto">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ClaveProdServ" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NoIdentificacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Cantidad" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ClaveUnidad" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Unidad" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Descripcion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ValorUnitario" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Importe" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Descuento" />
		</xsl:call-template>
		<xsl:for-each select="./cfdi:Impuestos/cfdi:Traslados/cfdi:Traslado">
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@Base" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@Impuesto" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@TipoFactor" />
			</xsl:call-template>
			<xsl:call-template name="Opcional">
				<xsl:with-param name="valor" select="./@TasaOCuota" />
			</xsl:call-template>
			<xsl:call-template name="Opcional">
				<xsl:with-param name="valor" select="./@Importe" />
			</xsl:call-template>
		</xsl:for-each>
		<xsl:for-each select="./cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion">
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@Base" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@Impuesto" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@TipoFactor" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@TasaOCuota" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@Importe" />
			</xsl:call-template>
		</xsl:for-each>
		<xsl:for-each select="./cfdi:InformacionAduanera">
			<xsl:apply-templates select="." />
		</xsl:for-each>
		<xsl:if test="./cfdi:CuentaPredial">
			<xsl:apply-templates select="./cfdi:CuentaPredial" />
		</xsl:if>
		<xsl:if test="./cfdi:ComplementoConcepto">
			<xsl:apply-templates select="./cfdi:ComplementoConcepto" />
		</xsl:if>
		<xsl:for-each select=".//cfdi:Parte">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="cfdi:InformacionAduanera">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@NumeroPedimento" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="cfdi:CuentaPredial">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Numero" />
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="cfdi:ComplementoConcepto">
		<xsl:for-each select="./*">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="cfdi:Parte">
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@ClaveProdServ" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@NoIdentificacion" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Cantidad" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Unidad" />
		</xsl:call-template>
		<xsl:call-template name="Requerido">
			<xsl:with-param name="valor" select="./@Descripcion" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@ValorUnitario" />
		</xsl:call-template>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@Importe" />
		</xsl:call-template>
		<xsl:for-each select=".//cfdi:InformacionAduanera">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="cfdi:Complemento">
		<xsl:for-each select="./*">
			<xsl:apply-templates select="." />
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="cfdi:Impuestos">
		<xsl:for-each select="./cfdi:Retenciones/cfdi:Retencion">
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@Impuesto" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@Importe" />
			</xsl:call-template>
		</xsl:for-each>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalImpuestosRetenidos" />
		</xsl:call-template>
		<xsl:for-each select="./cfdi:Traslados/cfdi:Traslado">
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@Impuesto" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@TipoFactor" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@TasaOCuota" />
			</xsl:call-template>
			<xsl:call-template name="Requerido">
				<xsl:with-param name="valor" select="./@Importe" />
			</xsl:call-template>
		</xsl:for-each>
		<xsl:call-template name="Opcional">
			<xsl:with-param name="valor" select="./@TotalImpuestosTrasladados" />
		</xsl:call-template>
	</xsl:template>
</xsl:stylesheet>