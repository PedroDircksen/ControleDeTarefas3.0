insert into TBCompromissos 
	(	
		[Id],
		[Assunto], 
		[Local], 
		[DataCompromisso], 
		[HoraInicio], 
		[HoraTermino], 
		[Id_Contato]
	) 
	values 
	(		
		1,
		'Business', 
		'Restaurante',
		'06/01/2000',
		'23:59',
		'23:59',
		4
	)
	SELECT SCOPE_IDENTITY();

	
SELECT 
                    F.ID,
                    F.ASSUNTO, 
		            F.LOCAL, 
		            F.DATACOMPROMISSO, 
		            F.HORAINICIO, 
		            F.HORATERMINO, 
		            C.NOME
                FROM 
                    TBCOMPROMISSOS F LEFT JOIN
					TBContatos C
				ON
					F.Id_Contato = C.ID


select * from TBCompromissos