using Idiomind.Domain.Simulations.Entities;
using Idiomind.Infrastructure.Seeding;
using Idiomind.Infrastructure.Seeding.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Idiomind.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed_InitialContent : Migration
    {
            /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedAeroportoCategory(migrationBuilder);
            SeedHospedagemCategory(migrationBuilder);
            SeedAlimentacaoCategory(migrationBuilder);
            SeedTransporteCategory(migrationBuilder);
            SeedComprasCategory(migrationBuilder);
            SeedEmergenciasCategory(migrationBuilder);
            SeedTurismoCategory(migrationBuilder);
            SeedDiaADiaCategory(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }

        private static void SeedAeroportoCategory(MigrationBuilder migration)
        {
            const string categorySlug = "aeroporto";
            migration.InsertCategory(categorySlug, new Category(
                icon: "flight",
                translations: [new("pt-BR", "Aeroporto & voo"), new("en-US", "Airport & flight"), new("fr-FR", "A?roport et vol"), new("it-IT", "Aeroporto e volo")]
            ));

            var categoryId = ContentId.From(categorySlug);

            // Imigração
            const string situacao1Slug = "imigracao";
            migration.InsertSituation(situacao1Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Passando pela imigração", "Evite respostas vagas que podem gerar dúvidas e atrasar sua entrada no país."), new("en-US", "Going through immigration", "Avoid vague answers that can raise doubts and delay your entry into the country."), new("fr-FR", "Passer l'immigration", "?vitez les r?ponses vagues qui peuvent cr?er des doutes et retarder votre entr?e dans le pays."), new("it-IT", "Passare l'immigrazione", "Evita risposte vaghe che possono creare dubbi e ritardare il tuo ingresso nel paese.")]
            ));

            var situacao1Id = ContentId.From(situacao1Slug);

            // Imigracao Turismo
            const string variant1Slug = "imigracao-turismo";
            migration.InsertSituationVariant(variant1Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are a US immigration officer at Miami airport. You are professional, direct, and skeptical of tourists. Ask questions in a serious tone, speak clearly, and wait for complete answers. Do not accept vague responses. Your role is to verify: travel purpose, duration, accommodation, funds, and whether the person should be admitted. Be formal and don't make small talk. If the person seems suspicious or gives contradictory answers, ask follow-up questions. You can approve entry if satisfied, or deny it if concerned.""",
                initialMessage: "Next! Step forward. Can I see your passport, please?",
                translations: [new("pt-BR", "Turismo por 2 semanas", "Você acabou de pousar em Miami. O agente da imigração chama você ao guichê. Ele parece sério, faz perguntas secas e espera respostas diretas."), new("en-US", "Tourism for 2 weeks", "You have just landed in Miami. The immigration officer calls you to the booth. He looks serious, asks blunt questions, and expects direct answers."), new("fr-FR", "Tourisme pendant 2 semaines", "Vous venez d'atterrir ? Miami. L'agent d'immigration vous appelle au guichet. Il semble s?rieux, pose des questions s?ches et attend des r?ponses directes."), new("it-IT", "Turismo per 2 settimane", "Sei appena atterrato a Miami. L'agente dell'immigrazione ti chiama allo sportello. Sembra serio, fa domande secche e si aspetta risposte dirette.")]
            ));

            var variant1Id = ContentId.From(variant1Slug);
            migration.InsertObjective($"{variant1Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Informar o propósito da visita como turismo"), new("en-US", "State the purpose of the visit as tourism"), new("fr-FR", "Indiquer que le but de la visite est le tourisme"), new("it-IT", "Indicare lo scopo della visita come turismo")]));
            migration.InsertObjective($"{variant1Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Dizer quanto tempo você vai ficar no país"), new("en-US", "Say how long you will stay in the country"), new("fr-FR", "Dire combien de temps vous allez rester dans le pays"), new("it-IT", "Dire quanto tempo resterai nel paese")]));
            migration.InsertObjective($"{variant1Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Informar onde você está hospedado"), new("en-US", "Say where you are staying"), new("fr-FR", "Indiquer o? vous ?tes h?berg?"), new("it-IT", "Dire dove alloggi")]));
            migration.InsertObjective($"{variant1Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Responder sobre quanto dinheiro você está trazendo"), new("en-US", "Answer how much money you are bringing"), new("fr-FR", "R?pondre sur la somme d'argent que vous apportez"), new("it-IT", "Rispondere su quanti soldi stai portando")]));
            migration.InsertObjective($"{variant1Slug}:obj:5", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Encerrar a conversa de forma educada ao ser liberado pelo agente da imigração"), new("en-US", "End the conversation politely when released by the immigration officer"), new("fr-FR", "Terminer la conversation poliment lorsque l'agent d'immigration vous laisse passer"), new("it-IT", "Concludere la conversazione educatamente quando l'agente dell'immigrazione ti lascia passare")]));

            // Imigracao Familiar
            const string variant2Slug = "imigracao-familiar";
            migration.InsertSituationVariant(variant2Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are a US immigration officer at Orlando airport. You are professional but more curious about family visits than tourist visits. Ask questions about the relationship with the family member, how long they've known each other, and confirm the address where they'll stay. Be thorough but not hostile. Speak clearly and wait for complete answers. Your goal is to verify the person's credibility and whether the visit is legitimate.""",
                initialMessage: "Good morning. I see here you're visiting a family member. Who is this person, and how do you know them?",
                translations: [new("pt-BR", "Visita a familiar", "Você vai visitar um primo que mora em Orlando. O agente pergunta sobre sua estadia, relacionamento com o familiar e endereço de onde vai ficar."), new("en-US", "Visiting a relative", "You are visiting a cousin who lives in Orlando. The officer asks about your stay, your relationship with the relative, and the address where you will stay."), new("fr-FR", "Visite ? un proche", "Vous allez rendre visite ? un cousin qui vit ? Orlando. L'agent vous interroge sur votre s?jour, votre lien avec ce proche et l'adresse o? vous logerez."), new("it-IT", "Visita a un familiare", "Vai a trovare un cugino che vive a Orlando. L'agente ti chiede del soggiorno, del rapporto con il familiare e dell'indirizzo dove alloggerai.")]
            ));

            var variant2Id = ContentId.From(variant2Slug);
            migration.InsertObjective($"{variant2Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Explicar que vai visitar um parente"), new("en-US", "Explain that you are visiting a relative"), new("fr-FR", "Expliquer que vous rendez visite ? un proche"), new("it-IT", "Spiegare che stai visitando un parente")]));
            migration.InsertObjective($"{variant2Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Informar o endereço onde vai se hospedar"), new("en-US", "Provide the address where you will stay"), new("fr-FR", "Indiquer l'adresse o? vous allez loger"), new("it-IT", "Fornire l'indirizzo dove alloggerai")]));
            migration.InsertObjective($"{variant2Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Responder perguntas sobre o grau de parentesco"), new("en-US", "Answer questions about the family relationship"), new("fr-FR", "R?pondre aux questions sur le degr? de parent?"), new("it-IT", "Rispondere alle domande sul grado di parentela")]));
            migration.InsertObjective($"{variant2Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Confirmar data de retorno ao Brasil"), new("en-US", "Confirm your return date to Brazil"), new("fr-FR", "Confirmer votre date de retour au Br?sil"), new("it-IT", "Confermare la data di ritorno in Brasile")]));

            // Imigracao Conexao
            const string variant3Slug = "imigracao-conexao";
            migration.InsertSituationVariant(variant3Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are a US immigration officer at JFK airport checking a passenger with a connecting flight. You are efficient and formal. Verify their final destination, check their documentation (visa, passport, return ticket), and understand their travel itinerary. Ask direct questions and expect clear answers. Your role is to ensure they have proper documents and their story is consistent. Be professional and don't rush them, but stay focused on verification.""",
                initialMessage: "Good morning. I see you're connecting to Los Angeles. Let me see your passport and boarding pass, please.",
                translations: [new("pt-BR", "Conexão para outro estado", "Você pousou em Nova York mas seu destino final é Los Angeles. O agente precisa entender seu roteiro e confirmar sua documentação."), new("en-US", "Connection to another state", "You landed in New York, but your final destination is Los Angeles. The officer needs to understand your itinerary and confirm your documents."), new("fr-FR", "Correspondance vers un autre ?tat", "Vous avez atterri ? New York, mais votre destination finale est Los Angeles. L'agent doit comprendre votre itin?raire et v?rifier vos documents."), new("it-IT", "Coincidenza per un altro stato", "Sei atterrato a New York, ma la tua destinazione finale ? Los Angeles. L'agente deve capire il tuo itinerario e confermare i tuoi documenti.")]
            ));

            var variant3Id = ContentId.From(variant3Slug);
            migration.InsertObjective($"{variant3Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Explicar que você está em conexão"), new("en-US", "Explain that you are in transit"), new("fr-FR", "Expliquer que vous ?tes en correspondance"), new("it-IT", "Spiegare che sei in transito")]));
            migration.InsertObjective($"{variant3Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Informar seu destino final"), new("en-US", "State your final destination"), new("fr-FR", "Indiquer votre destination finale"), new("it-IT", "Indicare la tua destinazione finale")]));
            migration.InsertObjective($"{variant3Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Informar quais documentos você possui quando solicitado pelo agente"), new("en-US", "Say which documents you have when the officer asks"), new("fr-FR", "Indiquer les documents que vous avez lorsque l'agent les demande"), new("it-IT", "Dire quali documenti hai quando richiesti dall'agente")]));
            migration.InsertObjective($"{variant3Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Responder sobre a duração total da viagem"), new("en-US", "Answer about the total length of the trip"), new("fr-FR", "R?pondre sur la dur?e totale du voyage"), new("it-IT", "Rispondere sulla durata totale del viaggio")]));

            // Check-in e bagagem
            const string situacao2Slug = "checkin-aeroporto";
            migration.InsertSituation(situacao2Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Check-in e bagagem", "Evite taxas inesperadas e confusão no embarque ao confirmar malas, assento e portão."), new("en-US", "Check-in and baggage", "Avoid unexpected fees and boarding confusion by confirming bags, seat, and gate."), new("fr-FR", "Enregistrement et bagages", "?vitez les frais impr?vus et la confusion ? l'embarquement en confirmant les bagages, le si?ge et la porte."), new("it-IT", "Check-in e bagagli", "Evita costi imprevisti e confusione all'imbarco confermando bagagli, posto e gate.")]
            ));

            var situacao2Id = ContentId.From(situacao2Slug);

            // Check-in Normal
            const string variant4Slug = "checkin-normal";
            migration.InsertSituationVariant(variant4Slug, new SituationVariant(
                situationId: situacao2Id,
                learningLanguage: "en",
                promptInstructions: """You are a check-in agent at an airline counter. You are friendly and efficient. Guide the passenger through the check-in process: confirm their name and flight details, weigh their luggage, offer seat preferences (window or aisle), confirm the number of checked bags, and provide gate information. Speak clearly and at a normal pace. Be helpful and answer any questions about the flight or luggage policies. Stay professional and follow standard procedures.""",
                initialMessage: "Good morning! Welcome to [Airline]. Can I see your passport and booking confirmation, please?",
                translations: [new("pt-BR", "Despacho normal", "Você está no balcão de check-in da companhia aérea. O atendente vai pesar sua bagagem e perguntar sobre o assento."), new("en-US", "Regular baggage drop", "You are at the airline check-in counter. The agent will weigh your baggage and ask about your seat."), new("fr-FR", "D?p?t de bagage normal", "Vous ?tes au comptoir d'enregistrement de la compagnie a?rienne. L'agent va peser votre bagage et vous poser des questions sur votre si?ge."), new("it-IT", "Consegna bagagli normale", "Sei al banco check-in della compagnia aerea. L'addetto peser? il bagaglio e ti chieder? del posto.")]
            ));

            var variant4Id = ContentId.From(variant4Slug);
            migration.InsertObjective($"{variant4Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Confirmar seu nome e os dados do voo ao se identificar no balcão"), new("en-US", "Confirm your name and flight details when identifying yourself at the counter"), new("fr-FR", "Confirmer votre nom et les d?tails du vol en vous pr?sentant au comptoir"), new("it-IT", "Confermare il tuo nome e i dati del volo quando ti presenti al banco")]));
            migration.InsertObjective($"{variant4Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Solicitar assento de janela ou corredor"), new("en-US", "Request a window or aisle seat"), new("fr-FR", "Demander un si?ge c?t? fen?tre ou c?t? couloir"), new("it-IT", "Richiedere un posto finestrino o corridoio")]));
            migration.InsertObjective($"{variant4Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Confirmar o número de malas despachadas"), new("en-US", "Confirm the number of checked bags"), new("fr-FR", "Confirmer le nombre de bagages enregistr?s"), new("it-IT", "Confermare il numero di bagagli da imbarcare")]));
            migration.InsertObjective($"{variant4Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Perguntar sobre o portão de embarque"), new("en-US", "Ask about the boarding gate"), new("fr-FR", "Demander la porte d'embarquement"), new("it-IT", "Chiedere il gate di imbarco")]));

            // Check-in Excesso
            const string variant5Slug = "checkin-excesso";
            migration.InsertSituationVariant(variant5Slug, new SituationVariant(
                situationId: situacao2Id,
                learningLanguage: "en",
                promptInstructions: """You are a check-in agent explaining an overweight baggage charge to a passenger. You are empathetic but firm about the rules. Explain the weight limit, the excess charge per kilogram, and offer solutions: the passenger can remove items, pay the fee, or use the carry-on allowance. Speak clearly and give them time to decide. Answer questions about the policy. Be professional and helpful, not aggressive.""",
                initialMessage: "I see your suitcase is 28 kilograms. The limit is 23 kilograms. You'll need to pay an excess baggage fee or redistribute your items. What would you like to do?",
                translations: [new("pt-BR", "Bagagem acima do peso", "Sua mala pesou 28kg — 5kg acima do limite. O atendente te informa e explica as opções."), new("en-US", "Overweight baggage", "Your suitcase weighed 28 kg, 5 kg over the limit. The agent informs you and explains the options."), new("fr-FR", "Bagage en surpoids", "Votre valise p?se 28 kg, soit 5 kg au-dessus de la limite. L'agent vous informe et explique les options."), new("it-IT", "Bagaglio in sovrappeso", "La tua valigia pesa 28 kg, 5 kg oltre il limite. L'addetto ti informa e spiega le opzioni.")]
            ));

            var variant5Id = ContentId.From(variant5Slug);
            migration.InsertObjective($"{variant5Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant5Id, translations: [new("pt-BR", "Entender o valor da taxa de excesso"), new("en-US", "Understand the excess baggage fee"), new("fr-FR", "Comprendre le montant des frais d'exc?dent de bagage"), new("it-IT", "Capire l'importo della tariffa per eccesso bagaglio")]));
            migration.InsertObjective($"{variant5Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant5Id, translations: [new("pt-BR", "Perguntar se é possível transferir itens para a mala de mão"), new("en-US", "Ask if you can move items to your carry-on bag"), new("fr-FR", "Demander s'il est possible de transf?rer des affaires dans le bagage ? main"), new("it-IT", "Chiedere se ? possibile spostare oggetti nel bagaglio a mano")]));
            migration.InsertObjective($"{variant5Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant5Id, translations: [new("pt-BR", "Confirmar o pagamento da taxa e verificar se o valor cobrado está correto"), new("en-US", "Confirm payment of the fee and check that the amount charged is correct"), new("fr-FR", "Confirmer le paiement des frais et v?rifier que le montant factur? est correct"), new("it-IT", "Confermare il pagamento della tariffa e verificare che l'importo addebitato sia corretto")]));
            migration.InsertObjective($"{variant5Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant5Id, translations: [new("pt-BR", "Confirmar que a bagagem está despachada corretamente"), new("en-US", "Confirm that the baggage was checked correctly"), new("fr-FR", "Confirmer que le bagage a bien ?t? enregistr?"), new("it-IT", "Confermare che il bagaglio sia stato imbarcato correttamente")]));

            // A bordo do avião
            const string situacao3Slug = "bordo";
            migration.InsertSituation(situacao3Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "A bordo do avião", "Peça comida, bebida ou ajuda sem depender de gestos quando algo faltar no voo."), new("en-US", "On board the plane", "Ask for food, drinks, or help without relying on gestures when something is missing during the flight."), new("fr-FR", "? bord de l'avion", "Demandez de la nourriture, une boisson ou de l'aide sans d?pendre des gestes lorsqu'il vous manque quelque chose pendant le vol."), new("it-IT", "A bordo dell'aereo", "Chiedi cibo, bevande o aiuto senza dipendere dai gesti quando manca qualcosa durante il volo.")]
            ));

            var situacao3Id = ContentId.From(situacao3Slug);

            // Bordo Refeição
            const string variant6Slug = "bordo-refeicao";
            migration.InsertSituationVariant(variant6Slug, new SituationVariant(
                situationId: situacao3Id,
                learningLanguage: "en",
                promptInstructions: """You are a flight attendant pushing a food and beverage cart down the aisle. You are friendly and efficient. Offer meal options, describe them clearly, and wait for the passenger's choice. Answer questions about ingredients or alternatives. Process their order quickly and professionally. Offer beverages. Be warm but professional, and handle any dietary restrictions politely. Move at a realistic pace—you have other passengers to serve.""",
                initialMessage: "Good afternoon! Can I interest you in a snack? We have a chicken sandwich or a vegetarian pasta today.",
                translations: [new("pt-BR", "Pedido de refeição", "O comissário passa pelo corredor com o carrinho de refeições e oferece as opções disponíveis."), new("en-US", "Meal order", "The flight attendant comes down the aisle with the meal cart and offers the available options."), new("fr-FR", "Commande de repas", "Le membre d'?quipage passe dans l'all?e avec le chariot de repas et propose les options disponibles."), new("it-IT", "Ordine del pasto", "L'assistente di volo passa nel corridoio con il carrello dei pasti e offre le opzioni disponibili.")]
            ));

            var variant6Id = ContentId.From(variant6Slug);
            migration.InsertObjective($"{variant6Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant6Id, translations: [new("pt-BR", "Entender as opções de prato oferecidas"), new("en-US", "Understand the dish options offered"), new("fr-FR", "Comprendre les options de plats propos?es"), new("it-IT", "Capire le opzioni di piatto offerte")]));
            migration.InsertObjective($"{variant6Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant6Id, translations: [new("pt-BR", "Escolher e confirmar seu pedido"), new("en-US", "Choose and confirm your order"), new("fr-FR", "Choisir et confirmer votre commande"), new("it-IT", "Scegliere e confermare il tuo ordine")]));
            migration.InsertObjective($"{variant6Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant6Id, translations: [new("pt-BR", "Pedir uma bebida adicional"), new("en-US", "Ask for an additional drink"), new("fr-FR", "Demander une boisson suppl?mentaire"), new("it-IT", "Chiedere una bevanda in pi?")]));
            migration.InsertObjective($"{variant6Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant6Id, translations: [new("pt-BR", "Agradecer de forma natural"), new("en-US", "Thank them naturally"), new("fr-FR", "Remercier de fa?on naturelle"), new("it-IT", "Ringraziare in modo naturale")]));
        }

        private static void SeedHospedagemCategory(MigrationBuilder migration)
        {
            const string categorySlug = "hospedagem";
            migration.InsertCategory(categorySlug, new Category(
                icon: "hotel",
                translations: [new("pt-BR", "Hospedagem"), new("en-US", "Accommodation"), new("fr-FR", "H?bergement"), new("it-IT", "Alloggio")]
            ));

            var categoryId = ContentId.From(categorySlug);

            // Check-in no hotel
            const string situacao1Slug = "checkin-hotel";
            migration.InsertSituation(situacao1Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Check-in no hotel", "Evite erro na reserva e entenda caução, quarto e horários antes de subir."), new("en-US", "Hotel check-in", "Avoid reservation mistakes and understand deposit, room, and schedules before going upstairs."), new("fr-FR", "Arriv?e ? l'h?tel", "?vitez les erreurs de r?servation et comprenez la caution, la chambre et les horaires avant de monter."), new("it-IT", "Check-in in hotel", "Evita errori nella prenotazione e capisci cauzione, camera e orari prima di salire.")]
            ));

            var situacao1Id = ContentId.From(situacao1Slug);

            // Check-in padrão
            const string variant1Slug = "checkin-hotel-padrao";
            migration.InsertSituationVariant(variant1Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are a friendly hotel receptionist checking in a guest after a long flight. You are warm, professional, and speak clearly. Guide them through check-in: confirm their name and reservation, explain the credit card hold for incidentals, provide information about breakfast hours, and confirm their room number and access instructions. Answer questions about WiFi, amenities, and the local area. Speak at a comfortable pace and be reassuring—they are tired from travel.""",
                initialMessage: "Welcome! We're so glad you're here. Let me get you checked in. Can I see your ID and the confirmation email, please?",
                translations: [new("pt-BR", "Check-in padrão", "Você acabou de chegar ao hotel após um voo longo. É noite, você está cansado. A recepcionista é simpática mas fala rápido."), new("en-US", "Standard check-in", "You have just arrived at the hotel after a long flight. It is night, you are tired. The receptionist is friendly but speaks quickly."), new("fr-FR", "Arriv?e standard", "Vous venez d'arriver ? l'h?tel apr?s un long vol. Il fait nuit, vous ?tes fatigu?. La r?ceptionniste est sympathique mais parle vite."), new("it-IT", "Check-in standard", "Sei appena arrivato in hotel dopo un lungo volo. ? sera, sei stanco. La receptionist ? gentile ma parla velocemente.")]
            ));

            var variant1Id = ContentId.From(variant1Slug);
            migration.InsertObjective($"{variant1Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Informar seu nome e confirmar a reserva"), new("en-US", "Give your name and confirm the reservation"), new("fr-FR", "Donner votre nom et confirmer la r?servation"), new("it-IT", "Dire il tuo nome e confermare la prenotazione")]));
            migration.InsertObjective($"{variant1Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Confirmar que irá fornecer um cartão de crédito como garantia para a caução"), new("en-US", "Confirm that you will provide a credit card as a deposit guarantee"), new("fr-FR", "Confirmer que vous fournirez une carte de cr?dit comme garantie pour la caution"), new("it-IT", "Confermare che fornirai una carta di credito come garanzia per la cauzione")]));
            migration.InsertObjective($"{variant1Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Perguntar sobre o horário do café da manhã"), new("en-US", "Ask about breakfast hours"), new("fr-FR", "Demander les horaires du petit-d?jeuner"), new("it-IT", "Chiedere l'orario della colazione")]));
            migration.InsertObjective($"{variant1Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Confirmar o número do quarto e as instruções de acesso"), new("en-US", "Confirm the room number and access instructions"), new("fr-FR", "Confirmer le num?ro de chambre et les instructions d'acc?s"), new("it-IT", "Confermare il numero della camera e le istruzioni di accesso")]));
            migration.InsertObjective($"{variant1Slug}:obj:5", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Perguntar sobre o Wi-Fi"), new("en-US", "Ask about Wi-Fi"), new("fr-FR", "Demander des informations sur le Wi-Fi"), new("it-IT", "Chiedere informazioni sul Wi-Fi")]));

            // Check-in troca
            const string variant2Slug = "checkin-hotel-troca";
            migration.InsertSituationVariant(variant2Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are a hotel receptionist dealing with a guest complaint about their room allocation. You are empathetic and solution-focused. Listen to their concerns about the room (noise, no view, etc.). Acknowledge their disappointment, apologize, and work toward a resolution. Check system availability and offer alternatives. Be professional and avoid being defensive. The goal is to make the guest satisfied and restore confidence in their stay.""",
                initialMessage: "I understand you're not satisfied with your room. I sincerely apologize. Let me see what we can do to make this right. Can you tell me what the issue is?",
                translations: [new("pt-BR", "Quarto diferente do reservado", "O hotel te aloca em um quarto diferente do que você pagou — sem vista e mais barulhento. Você quer reclamar de forma educada."), new("en-US", "Room different from the one booked", "The hotel gives you a different room from the one you paid for, with no view and more noise. You want to complain politely."), new("fr-FR", "Chambre diff?rente de celle r?serv?e", "L'h?tel vous attribue une chambre diff?rente de celle que vous avez pay?e, sans vue et plus bruyante. Vous voulez vous plaindre poliment."), new("it-IT", "Camera diversa da quella prenotata", "L'hotel ti assegna una camera diversa da quella pagata, senza vista e pi? rumorosa. Vuoi reclamare in modo educato.")]
            ));

            var variant2Id = ContentId.From(variant2Slug);
            migration.InsertObjective($"{variant2Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Explicar o problema com o quarto atual"), new("en-US", "Explain the problem with the current room"), new("fr-FR", "Expliquer le probl?me avec la chambre actuelle"), new("it-IT", "Spiegare il problema con la camera attuale")]));
            migration.InsertObjective($"{variant2Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Citar os detalhes da reserva original (tipo de quarto, vista, preço) para embasar o pedido de troca"), new("en-US", "Mention the original reservation details (room type, view, price) to support the room-change request"), new("fr-FR", "Citer les d?tails de la r?servation originale (type de chambre, vue, prix) pour appuyer la demande de changement"), new("it-IT", "Citare i dettagli della prenotazione originale (tipo di camera, vista, prezzo) per sostenere la richiesta di cambio")]));
            migration.InsertObjective($"{variant2Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Pedir para trocar de quarto"), new("en-US", "Ask to change rooms"), new("fr-FR", "Demander ? changer de chambre"), new("it-IT", "Chiedere di cambiare camera")]));
            migration.InsertObjective($"{variant2Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Entender se há algum custo adicional"), new("en-US", "Understand whether there is any extra cost"), new("fr-FR", "Comprendre s'il y a un co?t suppl?mentaire"), new("it-IT", "Capire se c'? un costo aggiuntivo")]));
            migration.InsertObjective($"{variant2Slug}:obj:5", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Fechar o acordo educadamente"), new("en-US", "Close the agreement politely"), new("fr-FR", "Conclure l'accord poliment"), new("it-IT", "Chiudere l'accordo educatamente")]));

            // Pedidos ao quarto
            const string situacao2Slug = "solicitacoes-hotel";
            migration.InsertSituation(situacao2Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Pedidos ao quarto", "Resolva falta de itens ou problemas no quarto antes que atrapalhem sua estadia."), new("en-US", "Room requests", "Resolve missing items or room problems before they disrupt your stay."), new("fr-FR", "Demandes en chambre", "R?glez les objets manquants ou les probl?mes de chambre avant qu'ils ne g?nent votre s?jour."), new("it-IT", "Richieste in camera", "Risolvi mancanze o problemi in camera prima che disturbino il soggiorno.")]
            ));

            var situacao2Id = ContentId.From(situacao2Slug);

            // Itens básicos faltando
            const string variant3Slug = "solicitacoes-itens";
            migration.InsertSituationVariant(variant3Slug, new SituationVariant(
                situationId: situacao2Id,
                learningLanguage: "en",
                promptInstructions: """You are a hotel front desk staff answering a phone call from a guest in their room. You are helpful, courteous, and efficient. Listen to their request or problem—missing items, broken appliances, etc. Take notes, confirm what they need, and assure them of when housekeeping or maintenance will arrive. Be professional and empathetic. If they have an urgent issue, treat it as a priority.""",
                initialMessage: "Front desk speaking. How can I help you this evening?",
                translations: [new("pt-BR", "Itens básicos faltando", "Você está no quarto e percebe que falta toalha e o secador de cabelo não funciona. Você liga para a recepção."), new("en-US", "Missing basic items", "You are in the room and notice there is no towel and the hair dryer does not work. You call reception."), new("fr-FR", "Articles de base manquants", "Vous ?tes dans la chambre et remarquez qu'il manque une serviette et que le s?che-cheveux ne fonctionne pas. Vous appelez la r?ception."), new("it-IT", "Articoli essenziali mancanti", "Sei in camera e ti accorgi che manca un asciugamano e l'asciugacapelli non funziona. Chiami la reception.")]
            ));

            var variant3Id = ContentId.From(variant3Slug);
            migration.InsertObjective($"{variant3Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Identificar seu quarto ao ligar"), new("en-US", "Identify your room when calling"), new("fr-FR", "Indiquer votre chambre lors de l'appel"), new("it-IT", "Identificare la tua camera quando chiami")]));
            migration.InsertObjective($"{variant3Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Relatar o problema do secador"), new("en-US", "Report the problem with the hair dryer"), new("fr-FR", "Signaler le probl?me du s?che-cheveux"), new("it-IT", "Segnalare il problema dell'asciugacapelli")]));
            migration.InsertObjective($"{variant3Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Solicitar toalhas adicionais"), new("en-US", "Request additional towels"), new("fr-FR", "Demander des serviettes suppl?mentaires"), new("it-IT", "Richiedere asciugamani aggiuntivi")]));
            migration.InsertObjective($"{variant3Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Confirmar o prazo para o atendimento"), new("en-US", "Confirm the expected service time"), new("fr-FR", "Confirmer le d?lai d'intervention"), new("it-IT", "Confermare il tempo previsto per l'assistenza")]));

            // Check-out e conta final
            const string situacao3Slug = "checkout-hotel";
            migration.InsertSituation(situacao3Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Check-out e conta final", "Confira cobranças antes de pagar para não sair do hotel com valores indevidos."), new("en-US", "Check-out and final bill", "Check charges before paying so you do not leave the hotel with incorrect fees."), new("fr-FR", "D?part et facture finale", "V?rifiez les frais avant de payer pour ne pas quitter l'h?tel avec des montants incorrects."), new("it-IT", "Check-out e conto finale", "Controlla gli addebiti prima di pagare per non lasciare l'hotel con importi errati.")]
            ));

            var situacao3Id = ContentId.From(situacao3Slug);

            // Check-out padrão
            const string variant4Slug = "checkout-padrao";
            migration.InsertSituationVariant(variant4Slug, new SituationVariant(
                situationId: situacao3Id,
                learningLanguage: "en",
                promptInstructions: """You are a hotel receptionist processing a standard checkout. You are efficient and professional. Confirm the guest is checking out, verify their billing details, review the invoice with them (point out charges), and answer any questions about fees. Process the checkout quickly and pleasantly. Thank them for their stay and invite them back. If they mention any issues with the bill, address them calmly.""",
                initialMessage: "Good morning! Are you checking out today? Let me process that for you. I'll just need to review your final bill.",
                translations: [new("pt-BR", "Check-out sem pendências", "Você está saindo do hotel na manhã do último dia. Entrega as chaves e aguarda a fatura ser emitida."), new("en-US", "Check-out with no pending issues", "You are leaving the hotel on the morning of the last day. You hand over the keys and wait for the invoice to be issued."), new("fr-FR", "D?part sans probl?me", "Vous quittez l'h?tel le matin du dernier jour. Vous rendez les cl?s et attendez que la facture soit ?mise."), new("it-IT", "Check-out senza pendenze", "Stai lasciando l'hotel la mattina dell'ultimo giorno. Consegni le chiavi e aspetti che venga emessa la fattura.")]
            ));

            var variant4Id = ContentId.From(variant4Slug);
            migration.InsertObjective($"{variant4Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Informar que você está fazendo check-out"), new("en-US", "Say that you are checking out"), new("fr-FR", "Dire que vous faites le d?part"), new("it-IT", "Dire che stai facendo il check-out")]));
            migration.InsertObjective($"{variant4Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Verificar os itens cobrados na fatura"), new("en-US", "Check the items charged on the invoice"), new("fr-FR", "V?rifier les ?l?ments factur?s"), new("it-IT", "Verificare le voci addebitate in fattura")]));
            migration.InsertObjective($"{variant4Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Contestar alguma cobrança indevida se houver"), new("en-US", "Dispute any incorrect charge if there is one"), new("fr-FR", "Contester toute facturation incorrecte s'il y en a une"), new("it-IT", "Contestare eventuali addebiti non dovuti")]));
            migration.InsertObjective($"{variant4Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Pagar a conta final e solicitar recibo"), new("en-US", "Pay the final bill and request a receipt"), new("fr-FR", "Payer la facture finale et demander un re?u"), new("it-IT", "Pagare il conto finale e richiedere una ricevuta")]));
        }

        private static void SeedAlimentacaoCategory(MigrationBuilder migration)
        {
            const string categorySlug = "alimentacao";
            migration.InsertCategory(categorySlug, new Category(
                icon: "restaurant",
                translations: [new("pt-BR", "Alimentação"), new("en-US", "Food"), new("fr-FR", "Restauration"), new("it-IT", "Cibo")]
            ));

            var categoryId = ContentId.From(categorySlug);

            // Fast food
            const string situacao1Slug = "fastfood";
            migration.InsertSituation(situacao1Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Pedindo no fast food", "Faça pedidos e ajustes com clareza para evitar receber algo diferente do que queria."), new("en-US", "Ordering at fast food", "Order and make adjustments clearly to avoid getting something different from what you wanted."), new("fr-FR", "Commander dans un fast-food", "Commandez et demandez des ajustements clairement pour ?viter de recevoir autre chose que ce que vous vouliez."), new("it-IT", "Ordinare al fast food", "Ordina e chiedi modifiche con chiarezza per evitare di ricevere qualcosa di diverso da ci? che volevi.")]
            ));

            var situacao1Id = ContentId.From(situacao1Slug);

            // Fast food - Balcão
            const string variant1Slug = "fastfood-balcao";
            migration.InsertSituationVariant(variant1Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are a fast food cashier at a busy McDonald's counter. You are efficient and friendly. Take the customer's order, confirm each item, and repeat back the complete order. Answer questions about modifications (no onions, extra sauce, etc.). Confirm eat-in or take-out, state the total price, and ask for payment method. Move quickly but stay patient. Background noise is normal.""",
                initialMessage: "Hi! Welcome. What can I get for you today?",
                translations: [new("pt-BR", "Pedido simples no balcão", "Você está na fila do McDonald's. O caixa chama você e espera seu pedido. Há barulho ao redor."), new("en-US", "Simple counter order", "You are in line at McDonald's. The cashier calls you and waits for your order. There is noise around."), new("fr-FR", "Commande simple au comptoir", "Vous ?tes dans la file chez McDonald's. Le caissier vous appelle et attend votre commande. Il y a du bruit autour."), new("it-IT", "Ordine semplice al banco", "Sei in fila da McDonald's. Il cassiere ti chiama e aspetta il tuo ordine. C'? rumore intorno.")]
            ));

            var variant1Id = ContentId.From(variant1Slug);
            migration.InsertObjective($"{variant1Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Fazer o pedido de lanche, batata e bebida"), new("en-US", "Order a sandwich, fries, and a drink"), new("fr-FR", "Commander un sandwich, des frites et une boisson"), new("it-IT", "Ordinare panino, patatine e bevanda")]));
            migration.InsertObjective($"{variant1Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Solicitar modificações (sem cebola, molho extra)"), new("en-US", "Request modifications (no onion, extra sauce)"), new("fr-FR", "Demander des modifications (sans oignon, sauce en plus)"), new("it-IT", "Richiedere modifiche (senza cipolla, salsa extra)")]));
            migration.InsertObjective($"{variant1Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Responder se vai comer ali ou levar"), new("en-US", "Answer whether it is for here or to go"), new("fr-FR", "R?pondre si c'est sur place ou ? emporter"), new("it-IT", "Rispondere se mangi l? o da asporto")]));
            migration.InsertObjective($"{variant1Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Confirmar o pedido quando repetido pelo caixa"), new("en-US", "Confirm the order when the cashier repeats it"), new("fr-FR", "Confirmer la commande lorsque le caissier la r?p?te"), new("it-IT", "Confermare l'ordine quando il cassiere lo ripete")]));
            migration.InsertObjective($"{variant1Slug}:obj:5", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Confirmar o valor total e indicar a forma de pagamento"), new("en-US", "Confirm the total amount and indicate the payment method"), new("fr-FR", "Confirmer le montant total et indiquer le mode de paiement"), new("it-IT", "Confermare l'importo totale e indicare il metodo di pagamento")]));

            // Fast food - Drive-thru
            const string variant2Slug = "fastfood-drivethrough";
            migration.InsertSituationVariant(variant2Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are a drive-thru worker taking orders through an intercom. Communication is sometimes unclear due to audio quality. Speak clearly and at a steady pace. Take the order, confirm items, and ask about modifications and drink sizes. Repeat the order back for confirmation. If the customer is unclear, ask them to repeat. Provide the total at the pickup window. Be efficient and polite despite the audio challenges.""",
                initialMessage: "Hi, welcome to [Restaurant]. What would you like to order today?",
                translations: [new("pt-BR", "Drive-thru", "Você está de carro no drive-thru. A comunicação é pelo interfone — difícil ouvir, difícil ser ouvido."), new("en-US", "Drive-thru", "You are in the car at the drive-thru. Communication is through the speaker, hard to hear and hard to be heard."), new("fr-FR", "Drive", "Vous ?tes en voiture au drive. La communication se fait par l'interphone, difficile d'entendre et de se faire entendre."), new("it-IT", "Drive-through", "Sei in auto al drive-through. La comunicazione avviene tramite interfono: difficile sentire e farsi sentire.")]
            ));

            var variant2Id = ContentId.From(variant2Slug);
            migration.InsertObjective($"{variant2Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Falar o pedido claramente pelo interfone"), new("en-US", "Say the order clearly through the speaker"), new("fr-FR", "Dire la commande clairement par l'interphone"), new("it-IT", "Dire chiaramente l'ordine tramite interfono")]));
            migration.InsertObjective($"{variant2Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Pedir para repetir se não entender"), new("en-US", "Ask them to repeat if you do not understand"), new("fr-FR", "Demander de r?p?ter si vous ne comprenez pas"), new("it-IT", "Chiedere di ripetere se non capisci")]));
            migration.InsertObjective($"{variant2Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Confirmar se o pedido exibido na tela de confirmação está correto ou solicitar uma correção"), new("en-US", "Confirm whether the order shown on the confirmation screen is correct or request a correction"), new("fr-FR", "Confirmer si la commande affich?e ? l'?cran est correcte ou demander une correction"), new("it-IT", "Confermare se l'ordine mostrato sullo schermo ? corretto o chiedere una correzione")]));
            migration.InsertObjective($"{variant2Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Confirmar o valor cobrado na janela de pagamento"), new("en-US", "Confirm the amount charged at the payment window"), new("fr-FR", "Confirmer le montant factur? ? la fen?tre de paiement"), new("it-IT", "Confermare l'importo addebitato alla finestra di pagamento")]));

            // Restaurante
            const string situacao2Slug = "restaurante";
            migration.InsertSituation(situacao2Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Jantar em restaurante", "Entenda o cardápio e a conta para pedir bem e evitar escolhas ou cobranças erradas."), new("en-US", "Dinner at a restaurant", "Understand the menu and the bill so you can order well and avoid wrong choices or charges."), new("fr-FR", "D?ner au restaurant", "Comprenez le menu et l'addition pour bien commander et ?viter les mauvais choix ou les erreurs de facturation."), new("it-IT", "Cena al ristorante", "Capisci il menu e il conto per ordinare bene ed evitare scelte o addebiti sbagliati.")]
            ));

            var situacao2Id = ContentId.From(situacao2Slug);

            // Restaurante - Almoço
            const string variant3Slug = "restaurante-almoco";
            migration.InsertSituationVariant(variant3Slug, new SituationVariant(
                situationId: situacao2Id,
                learningLanguage: "en",
                promptInstructions: """You are a server at a casual American restaurant. You are friendly and informative. Greet the guest warmly, seat them, offer water, and present the menu. Answer questions about dishes, explain specials, and help with dietary questions. Take the appetizer order, then the main course order. Check in on their satisfaction. Offer dessert and drinks. Be conversational but not intrusive. Speak clearly and at a comfortable pace.""",
                initialMessage: "Good afternoon! Welcome. How many are dining with us today?",
                translations: [new("pt-BR", "Almoço casual", "Você entra em um restaurante americano para o almoço. O garçom te recebe, oferece mesa e começa a apresentar o cardápio."), new("en-US", "Casual lunch", "You enter an American restaurant for lunch. The waiter greets you, offers a table, and starts presenting the menu."), new("fr-FR", "D?jeuner d?contract?", "Vous entrez dans un restaurant am?ricain pour d?jeuner. Le serveur vous accueille, propose une table et commence ? pr?senter le menu."), new("it-IT", "Pranzo informale", "Entri in un ristorante americano per pranzo. Il cameriere ti accoglie, offre un tavolo e inizia a presentare il menu.")]
            ));

            var variant3Id = ContentId.From(variant3Slug);
            migration.InsertObjective($"{variant3Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Pedir uma mesa para o número de pessoas"), new("en-US", "Ask for a table for the number of people"), new("fr-FR", "Demander une table pour le nombre de personnes"), new("it-IT", "Chiedere un tavolo per il numero di persone")]));
            migration.InsertObjective($"{variant3Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Fazer perguntas sobre pratos desconhecidos no menu"), new("en-US", "Ask questions about unfamiliar dishes on the menu"), new("fr-FR", "Poser des questions sur les plats inconnus du menu"), new("it-IT", "Fare domande sui piatti sconosciuti del menu")]));
            migration.InsertObjective($"{variant3Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Informar restrição alimentar se houver"), new("en-US", "Mention a dietary restriction if there is one"), new("fr-FR", "Signaler une restriction alimentaire s'il y en a une"), new("it-IT", "Informare di eventuali restrizioni alimentari")]));
            migration.InsertObjective($"{variant3Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Fazer o pedido completo com entrada e principal"), new("en-US", "Place the full order with starter and main course"), new("fr-FR", "Passer la commande compl?te avec entr?e et plat principal"), new("it-IT", "Fare l'ordine completo con antipasto e piatto principale")]));
            migration.InsertObjective($"{variant3Slug}:obj:5", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Pedir a conta e confirmar o que está incluído no valor"), new("en-US", "Ask for the bill and confirm what is included in the amount"), new("fr-FR", "Demander l'addition et confirmer ce qui est inclus dans le montant"), new("it-IT", "Chiedere il conto e confermare cosa ? incluso nell'importo")]));

            // Restaurante - Jantar
            const string variant4Slug = "restaurante-jantar";
            migration.InsertSituationVariant(variant4Slug, new SituationVariant(
                situationId: situacao2Id,
                learningLanguage: "en",
                promptInstructions: """You are a server at an upscale fine dining restaurant. You are polished, knowledgeable, and sophisticated. Greet the guest respectfully, present the wine list, offer aperitifs, and recommend signature dishes using descriptive language. Explain cooking methods and ingredients. Answer questions gracefully. Take orders attentively. Offer wine pairings. Check in discreetly between courses. Be formal but warm. Move with purpose and professionalism.""",
                initialMessage: "Good evening. Welcome. We're delighted to have you with us. May I offer you something to drink before we begin?",
                translations: [new("pt-BR", "Jantar em restaurante sofisticado", "Você foi convidado para um jantar em um restaurante mais formal. O ambiente é elegante e o garçom usa um vocabulário mais rebuscado."), new("en-US", "Dinner at a fine restaurant", "You were invited to dinner at a more formal restaurant. The setting is elegant and the waiter uses more refined vocabulary."), new("fr-FR", "D?ner dans un restaurant chic", "Vous ?tes invit? ? d?ner dans un restaurant plus formel. Le cadre est ?l?gant et le serveur utilise un vocabulaire plus recherch?."), new("it-IT", "Cena in un ristorante elegante", "Sei stato invitato a cena in un ristorante pi? formale. L'ambiente ? elegante e il cameriere usa un vocabolario pi? raffinato.")]
            ));

            var variant4Id = ContentId.From(variant4Slug);
            migration.InsertObjective($"{variant4Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Confirmar a reserva ao chegar"), new("en-US", "Confirm the reservation when you arrive"), new("fr-FR", "Confirmer la r?servation ? l'arriv?e"), new("it-IT", "Confermare la prenotazione all'arrivo")]));
            migration.InsertObjective($"{variant4Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Entender as recomendações do garçom"), new("en-US", "Understand the waiter's recommendations"), new("fr-FR", "Comprendre les recommandations du serveur"), new("it-IT", "Capire i consigli del cameriere")]));
            migration.InsertObjective($"{variant4Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Pedir que explique um prato que você não conhece"), new("en-US", "Ask them to explain a dish you do not know"), new("fr-FR", "Demander d'expliquer un plat que vous ne connaissez pas"), new("it-IT", "Chiedere di spiegare un piatto che non conosci")]));
            migration.InsertObjective($"{variant4Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Escolher vinho ou bebida adequada"), new("en-US", "Choose an appropriate wine or drink"), new("fr-FR", "Choisir un vin ou une boisson appropri?"), new("it-IT", "Scegliere un vino o una bevanda adatta")]));
            migration.InsertObjective($"{variant4Slug}:obj:5", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Entender a conta e fechar o pagamento"), new("en-US", "Understand the bill and complete the payment"), new("fr-FR", "Comprendre l'addition et finaliser le paiement"), new("it-IT", "Capire il conto e completare il pagamento")]));

            // Café
            const string situacao3Slug = "cafe";
            migration.InsertSituation(situacao3Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Pedindo café e snacks", "Peça do seu jeito mesmo com fila, tamanhos e opções rápidas no balcão."), new("en-US", "Ordering coffee and snacks", "Order your way even with a line, sizes, and quick counter options."), new("fr-FR", "Commander caf? et snacks", "Commandez ? votre fa?on m?me avec une file, des tailles et des options rapides au comptoir."), new("it-IT", "Ordinare caff? e snack", "Ordina a modo tuo anche con fila, formati e opzioni rapide al banco.")]
            ));

            var situacao3Id = ContentId.From(situacao3Slug);

            // Café - Cafeteria
            const string variant5Slug = "cafe-cafeteria";
            migration.InsertSituationVariant(variant5Slug, new SituationVariant(
                situationId: situacao3Id,
                learningLanguage: "en",
                promptInstructions: """You are a barista at a casual coffee shop. You are friendly and efficient. Greet customers warmly and present the menu. Explain drink sizes and options (hot/cold, milk choices, flavor shots). Answer questions about drinks. Confirm their order and any customizations. Take payment and provide the order ticket. Be personable but move at a reasonable pace to serve the line. Suggest pastries and snacks naturally.""",
                initialMessage: "Hi there! Welcome. What can I get started for you?",
                translations: [new("pt-BR", "Em uma cafeteria", "Você entra em uma cafeteria para tomar um café e um lanche. O atendente te recebe e aguarda o pedido."), new("en-US", "In a coffee shop", "You enter a coffee shop to have a coffee and a snack. The attendant greets you and waits for your order."), new("fr-FR", "Dans un caf?", "Vous entrez dans un caf? pour prendre un caf? et un en-cas. Le serveur vous accueille et attend votre commande."), new("it-IT", "In una caffetteria", "Entri in una caffetteria per prendere un caff? e uno snack. L'addetto ti saluta e aspetta il tuo ordine.")]
            ));

            var variant5Id = ContentId.From(variant5Slug);
            migration.InsertObjective($"{variant5Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant5Id, translations: [new("pt-BR", "Perguntar sobre as opções disponíveis"), new("en-US", "Ask about the available options"), new("fr-FR", "Demander quelles options sont disponibles"), new("it-IT", "Chiedere quali opzioni sono disponibili")]));
            migration.InsertObjective($"{variant5Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant5Id, translations: [new("pt-BR", "Pedir uma bebida e especificar como você a quer (quente, frio, com leite, sem açúcar etc.)"), new("en-US", "Order a drink and specify how you want it (hot, cold, with milk, no sugar, etc.)"), new("fr-FR", "Commander une boisson et pr?ciser comment vous la voulez (chaude, froide, avec du lait, sans sucre, etc.)"), new("it-IT", "Ordinare una bevanda e specificare come la vuoi (calda, fredda, con latte, senza zucchero, ecc.)")]));
            migration.InsertObjective($"{variant5Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant5Id, translations: [new("pt-BR", "Adicionar um item de comida ao pedido"), new("en-US", "Add a food item to the order"), new("fr-FR", "Ajouter un aliment ? la commande"), new("it-IT", "Aggiungere un alimento all'ordine")]));
            migration.InsertObjective($"{variant5Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant5Id, translations: [new("pt-BR", "Confirmar o pedido e efetuar o pagamento"), new("en-US", "Confirm the order and make the payment"), new("fr-FR", "Confirmer la commande et effectuer le paiement"), new("it-IT", "Confermare l'ordine ed effettuare il pagamento")]));
        }

        private static void SeedTransporteCategory(MigrationBuilder migration)
        {
            const string categorySlug = "transporte";
            migration.InsertCategory(categorySlug, new Category(
                icon: "directions-car",
                translations: [new("pt-BR", "Transporte"), new("en-US", "Transportation"), new("fr-FR", "Transport"), new("it-IT", "Trasporti")]
            ));

            var categoryId = ContentId.From(categorySlug);

            // Uber
            const string situacao1Slug = "uber";
            migration.InsertSituation(situacao1Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Uber e transporte por app", "Confirme local e rota para evitar desencontros, cancelamentos ou viagem errada."), new("en-US", "Uber and app-based transport", "Confirm pickup point and route to avoid missed connections, cancellations, or the wrong ride."), new("fr-FR", "Uber et transport par application", "Confirmez le lieu de prise en charge et l'itin?raire pour ?viter les rendez-vous manqu?s, les annulations ou le mauvais trajet."), new("it-IT", "Uber e trasporto tramite app", "Conferma punto di incontro e percorso per evitare incontri mancati, cancellazioni o corse sbagliate.")]
            ));

            var situacao1Id = ContentId.From(situacao1Slug);

            // Uber - Corrida padrão
            const string variant1Slug = "uber-corrida";
            migration.InsertSituationVariant(variant1Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are an Uber driver picking up a passenger. You are friendly and professional. Confirm the passenger's name, greet them warmly, confirm the destination, and engage in light conversation during the drive. Ask about their origin, if they're visiting, what brings them to the city. Be conversational but not intrusive. Offer amenities (water, charger, aux cord). Speak at a natural pace and adjust music/temperature if asked. Thank them at the end and wish them a good day.""",
                initialMessage: "Hi! Are you [Name]? Great, hop in. Your destination is [Address], is that correct?",
                translations: [new("pt-BR", "Corrida padrão", "Seu Uber chegou. O motorista confirma seu nome e pergunta sobre o destino. Durante o trajeto ele tenta puxar conversa."), new("en-US", "Standard ride", "Your Uber has arrived. The driver confirms your name and asks about the destination. During the ride, he tries to make small talk."), new("fr-FR", "Course standard", "Votre Uber est arriv?. Le chauffeur confirme votre nom et demande la destination. Pendant le trajet, il essaie de faire la conversation."), new("it-IT", "Corsa standard", "Il tuo Uber ? arrivato. L'autista conferma il tuo nome e chiede la destinazione. Durante il viaggio prova a fare conversazione.")]
            ));

            var variant1Id = ContentId.From(variant1Slug);
            migration.InsertObjective($"{variant1Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Confirmar seu nome ao entrar no carro"), new("en-US", "Confirm your name when getting into the car"), new("fr-FR", "Confirmer votre nom en entrant dans la voiture"), new("it-IT", "Confermare il tuo nome salendo in auto")]));
            migration.InsertObjective($"{variant1Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Confirmar o endereço de destino"), new("en-US", "Confirm the destination address"), new("fr-FR", "Confirmer l'adresse de destination"), new("it-IT", "Confermare l'indirizzo di destinazione")]));
            migration.InsertObjective($"{variant1Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Responder a small talk do motorista"), new("en-US", "Respond to the driver's small talk"), new("fr-FR", "R?pondre ? la conversation l?g?re du chauffeur"), new("it-IT", "Rispondere alla conversazione informale dell'autista")]));
            migration.InsertObjective($"{variant1Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Pedir para ligar o ar-condicionado se necessário"), new("en-US", "Ask to turn on the air conditioning if needed"), new("fr-FR", "Demander d'allumer la climatisation si n?cessaire"), new("it-IT", "Chiedere di accendere l'aria condizionata se necessario")]));
            migration.InsertObjective($"{variant1Slug}:obj:5", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Agradecer o motorista ao chegar ao destino"), new("en-US", "Thank the driver when arriving at the destination"), new("fr-FR", "Remercier le chauffeur en arrivant ? destination"), new("it-IT", "Ringraziare l'autista all'arrivo a destinazione")]));

            // Uber - Problema
            const string variant2Slug = "uber-problema";
            migration.InsertSituationVariant(variant2Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are an Uber driver who has entered the wrong destination into GPS. You realize mid-drive that something is off. When the passenger points out the error, acknowledge the mistake calmly and immediately work to correct it. Ask for clarification on the correct address, input it into the navigation app, and reassure them you're heading the right way now. Be professional and apologetic but not defensive. Confirm the route is correct before continuing.""",
                initialMessage: "I'm heading to [wrong address], right? Actually, wait... I think I input the wrong address. Let me check with you—where are we actually going?",
                translations: [new("pt-BR", "Endereço errado ou problema na corrida", "O motorista está indo para o endereço errado. Você precisa corrigir antes de chegar longe demais."), new("en-US", "Wrong address or ride issue", "The driver is going to the wrong address. You need to correct it before getting too far away."), new("fr-FR", "Mauvaise adresse ou probl?me de course", "Le chauffeur se dirige vers la mauvaise adresse. Vous devez corriger avant de vous ?loigner trop."), new("it-IT", "Indirizzo sbagliato o problema con la corsa", "L'autista sta andando all'indirizzo sbagliato. Devi correggere prima di allontanarti troppo.")]
            ));

            var variant2Id = ContentId.From(variant2Slug);
            migration.InsertObjective($"{variant2Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Perceber e sinalizar que o destino está errado"), new("en-US", "Notice and point out that the destination is wrong"), new("fr-FR", "Remarquer et signaler que la destination est incorrecte"), new("it-IT", "Accorgersi e segnalare che la destinazione ? sbagliata")]));
            migration.InsertObjective($"{variant2Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Explicar o endereço correto de forma clara"), new("en-US", "Explain the correct address clearly"), new("fr-FR", "Expliquer clairement la bonne adresse"), new("it-IT", "Spiegare chiaramente l'indirizzo corretto")]));
            migration.InsertObjective($"{variant2Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Lidar com a situação se o motorista não entender"), new("en-US", "Handle the situation if the driver does not understand"), new("fr-FR", "G?rer la situation si le chauffeur ne comprend pas"), new("it-IT", "Gestire la situazione se l'autista non capisce")]));
            migration.InsertObjective($"{variant2Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Confirmar que agora estão indo ao lugar certo"), new("en-US", "Confirm that they are now going to the right place"), new("fr-FR", "Confirmer qu'ils vont maintenant au bon endroit"), new("it-IT", "Confermare che ora stanno andando nel posto giusto")]));

            // Metrô
            const string situacao2Slug = "metro";
            migration.InsertSituation(situacao2Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Metrô e ônibus", "Compre a passagem certa e peça ajuda antes de pegar a linha ou o sentido errado."), new("en-US", "Subway and bus", "Buy the right ticket and ask for help before taking the wrong line or direction."), new("fr-FR", "M?tro et bus", "Achetez le bon ticket et demandez de l'aide avant de prendre la mauvaise ligne ou direction."), new("it-IT", "Metropolitana e autobus", "Compra il biglietto giusto e chiedi aiuto prima di prendere la linea o la direzione sbagliata.")]
            ));

            var situacao2Id = ContentId.From(situacao2Slug);

            // Metrô - Passagem
            const string variant3Slug = "metro-passagem";
            migration.InsertSituationVariant(variant3Slug, new SituationVariant(
                situationId: situacao2Id,
                learningLanguage: "en",
                promptInstructions: """You are a metro station attendant helping passengers. You are patient and knowledgeable. When a passenger asks for help with the ticketing machine, explain the steps clearly and point them to the machine. Answer questions about which line goes where, directions to transfers, how to read the map, and fares. Provide clear navigation instructions using simple language and landmarks. Be helpful but keep explanations brief so others can move through the station.""",
                initialMessage: "Hi there! Need help? I can show you how to use the machine or answer questions about the system.",
                translations: [new("pt-BR", "Comprando passagem no metrô", "Você está em uma estação de metrô e precisa comprar uma passagem na máquina automática. Um funcionário está por perto para ajudar."), new("en-US", "Buying a subway ticket", "You are in a subway station and need to buy a ticket from the automatic machine. An employee is nearby to help."), new("fr-FR", "Acheter un ticket de m?tro", "Vous ?tes dans une station de m?tro et devez acheter un ticket ? la machine automatique. Un employ? est ? proximit? pour aider."), new("it-IT", "Comprare un biglietto della metro", "Sei in una stazione della metropolitana e devi comprare un biglietto alla macchinetta. Un dipendente ? vicino per aiutare.")]
            ));

            var variant3Id = ContentId.From(variant3Slug);
            migration.InsertObjective($"{variant3Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Pedir ajuda ao funcionário para usar a máquina"), new("en-US", "Ask the employee for help using the machine"), new("fr-FR", "Demander de l'aide ? l'employ? pour utiliser la machine"), new("it-IT", "Chiedere aiuto al dipendente per usare la macchinetta")]));
            migration.InsertObjective($"{variant3Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Perguntar qual linha ou direção tomar para chegar ao destino"), new("en-US", "Ask which line or direction to take to reach the destination"), new("fr-FR", "Demander quelle ligne ou direction prendre pour arriver ? destination"), new("it-IT", "Chiedere quale linea o direzione prendere per arrivare a destinazione")]));
            migration.InsertObjective($"{variant3Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Entender instruções de baldeação (troca de linha)"), new("en-US", "Understand transfer instructions (changing lines)"), new("fr-FR", "Comprendre les instructions de correspondance (changement de ligne)"), new("it-IT", "Capire le istruzioni di cambio (cambio linea)")]));
            migration.InsertObjective($"{variant3Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Confirmar o ponto de desembarque correto"), new("en-US", "Confirm the correct stop to get off"), new("fr-FR", "Confirmer le bon arr?t o? descendre"), new("it-IT", "Confermare la fermata corretta in cui scendere")]));

            // Aluguel de carro
            const string situacao3Slug = "aluguel-carro";
            migration.InsertSituation(situacao3Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Alugando um carro", "Entenda seguro, caução e combustível antes de assinar algo que encareça a viagem."), new("en-US", "Renting a car", "Understand insurance, deposit, and fuel before signing something that makes the trip more expensive."), new("fr-FR", "Louer une voiture", "Comprenez l'assurance, la caution et le carburant avant de signer quelque chose qui rendrait le voyage plus cher."), new("it-IT", "Noleggiare un'auto", "Capisci assicurazione, cauzione e carburante prima di firmare qualcosa che renda il viaggio pi? costoso.")]
            ));

            var situacao3Id = ContentId.From(situacao3Slug);

            // Aluguel - Balcão
            const string variant4Slug = "aluguel-balcao";
            migration.InsertSituationVariant(variant4Slug, new SituationVariant(
                situationId: situacao3Id,
                learningLanguage: "en",
                promptInstructions: """You are a car rental agent at a Hertz counter. You are professional and detail-oriented. Greet the customer, verify their reservation, check their driver's license and passport, explain insurance options (CDW, LDW), explain fuel policy (full-to-full vs. prepay), confirm pickup/return dates and locations, and ask about GPS/additional equipment. Present upgrades naturally but don't be pushy. Answer all questions clearly. Process the transaction efficiently and go over the contract terms.""",
                initialMessage: "Welcome to Hertz! I see you have a reservation with us. Can I see your driver's license and passport, please?",
                translations: [new("pt-BR", "Balcão da locadora", "Você está no balcão da Hertz no aeroporto. O atendente verifica sua CNH, cartão e oferece seguros adicionais."), new("en-US", "Rental counter", "You are at the Hertz counter at the airport. The agent checks your driver's license, card, and offers additional insurance."), new("fr-FR", "Comptoir de location", "Vous ?tes au comptoir Hertz ? l'a?roport. L'agent v?rifie votre permis, votre carte et propose des assurances suppl?mentaires."), new("it-IT", "Banco dell'autonoleggio", "Sei al banco Hertz in aeroporto. L'addetto controlla patente, carta e offre assicurazioni aggiuntive.")]
            ));

            var variant4Id = ContentId.From(variant4Slug);
            migration.InsertObjective($"{variant4Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Informar os dados da sua habilitação e documento de identificação ao atendente"), new("en-US", "Provide your driver's license and ID information to the agent"), new("fr-FR", "Fournir les donn?es de votre permis et de votre pi?ce d'identit? ? l'agent"), new("it-IT", "Fornire all'addetto i dati della patente e del documento")]));
            migration.InsertObjective($"{variant4Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Entender as opções de seguro oferecidas"), new("en-US", "Understand the insurance options offered"), new("fr-FR", "Comprendre les options d'assurance propos?es"), new("it-IT", "Capire le opzioni di assicurazione offerte")]));
            migration.InsertObjective($"{variant4Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Perguntar sobre a política de combustível"), new("en-US", "Ask about the fuel policy"), new("fr-FR", "Demander la politique de carburant"), new("it-IT", "Chiedere informazioni sulla politica del carburante")]));
            migration.InsertObjective($"{variant4Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Confirmar a data e local de devolução"), new("en-US", "Confirm the return date and location"), new("fr-FR", "Confirmer la date et le lieu de restitution"), new("it-IT", "Confermare data e luogo di restituzione")]));
            migration.InsertObjective($"{variant4Slug}:obj:5", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Recusar ou aceitar upgrades educadamente"), new("en-US", "Politely decline or accept upgrades"), new("fr-FR", "Refuser ou accepter poliment les surclassements"), new("it-IT", "Rifiutare o accettare upgrade educatamente")]));
        }

        private static void SeedComprasCategory(MigrationBuilder migration)
        {
            const string categorySlug = "compras";
            migration.InsertCategory(categorySlug, new Category(
                icon: "shopping-bag",
                translations: [new("pt-BR", "Compras"), new("en-US", "Shopping"), new("fr-FR", "Achats"), new("it-IT", "Acquisti")]
            ));

            var categoryId = ContentId.From(categorySlug);

            // Loja de roupas
            const string situacao1Slug = "loja-roupas";
            migration.InsertSituation(situacao1Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Loja de roupas", "Pergunte tamanho, prova e troca para não comprar errado ou perder o desconto."), new("en-US", "Clothing store", "Ask about size, fitting, and exchanges so you do not buy the wrong item or miss the discount."), new("fr-FR", "Magasin de v?tements", "Demandez la taille, l'essayage et les ?changes pour ne pas acheter le mauvais article ou manquer la r?duction."), new("it-IT", "Negozio di abbigliamento", "Chiedi taglia, prova e cambio per non comprare l'articolo sbagliato o perdere lo sconto.")]
            ));

            var situacao1Id = ContentId.From(situacao1Slug);

            // Loja - Busca
            const string variant1Slug = "loja-busca";
            migration.InsertSituationVariant(variant1Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are a clothing store sales associate. You are helpful and attentive. When a customer approaches or seems to need help, greet them warmly and ask what they're looking for. Listen to their description, suggest items, explain available sizes and colors, and lead them to the fitting room. Answer questions about fabric, fit, and return policy. Process the sale at the register with a friendly attitude. Be knowledgeable and encouraging without being pushy.""",
                initialMessage: "Hi! Welcome! Can I help you find something today?",
                translations: [new("pt-BR", "Procurando um item específico", "Você está em uma loja de roupas e procura uma peça específica. Um atendente se aproxima para ajudar."), new("en-US", "Looking for a specific item", "You are in a clothing store looking for a specific piece. A salesperson approaches to help."), new("fr-FR", "Chercher un article pr?cis", "Vous ?tes dans un magasin de v?tements et cherchez une pi?ce pr?cise. Un vendeur s'approche pour aider."), new("it-IT", "Cercando un articolo specifico", "Sei in un negozio di abbigliamento e cerchi un capo specifico. Un commesso si avvicina per aiutare.")]
            ));

            var variant1Id = ContentId.From(variant1Slug);
            migration.InsertObjective($"{variant1Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Descrever o produto que você procura"), new("en-US", "Describe the product you are looking for"), new("fr-FR", "D?crire le produit que vous cherchez"), new("it-IT", "Descrivere il prodotto che stai cercando")]));
            migration.InsertObjective($"{variant1Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Perguntar sobre tamanhos disponíveis"), new("en-US", "Ask about available sizes"), new("fr-FR", "Demander les tailles disponibles"), new("it-IT", "Chiedere le taglie disponibili")]));
            migration.InsertObjective($"{variant1Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Pedir para experimentar"), new("en-US", "Ask to try it on"), new("fr-FR", "Demander ? l'essayer"), new("it-IT", "Chiedere di provarlo")]));
            migration.InsertObjective($"{variant1Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Perguntar sobre política de troca"), new("en-US", "Ask about the exchange policy"), new("fr-FR", "Demander la politique d'?change"), new("it-IT", "Chiedere informazioni sulla politica di cambio")]));
            migration.InsertObjective($"{variant1Slug}:obj:5", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Confirmar a compra e a forma de pagamento no caixa"), new("en-US", "Confirm the purchase and payment method at checkout"), new("fr-FR", "Confirmer l'achat et le mode de paiement ? la caisse"), new("it-IT", "Confermare l'acquisto e il metodo di pagamento alla cassa")]));

            // Loja - Troca
            const string variant2Slug = "loja-troca";
            migration.InsertSituationVariant(variant2Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are a customer service associate handling a return or exchange. You are professional and solution-focused. Listen to the customer's reason for return (doesn't fit, wrong color, defective). Check the receipt and item condition. Explain return policy (timeframe, condition requirements). Offer exchange or refund options based on policy. Process the transaction fairly and politely. If there's an issue with the policy, escalate to a manager but remain professional and empathetic.""",
                initialMessage: "Hi! I see you'd like to return this item. What's the reason for the return?",
                translations: [new("pt-BR", "Devolvendo ou trocando um item", "Você comprou uma camisa ontem mas ela não serviu bem. Volta à loja para trocar ou devolver."), new("en-US", "Returning or exchanging an item", "You bought a shirt yesterday, but it did not fit well. You return to the store to exchange it or get a refund."), new("fr-FR", "Retourner ou ?changer un article", "Vous avez achet? une chemise hier, mais elle ne vous va pas bien. Vous retournez au magasin pour l'?changer ou vous faire rembourser."), new("it-IT", "Restituire o cambiare un articolo", "Hai comprato una camicia ieri, ma non ti stava bene. Torni in negozio per cambiarla o restituirla.")]
            ));

            var variant2Id = ContentId.From(variant2Slug);
            migration.InsertObjective($"{variant2Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Explicar o motivo da devolução"), new("en-US", "Explain the reason for the return"), new("fr-FR", "Expliquer la raison du retour"), new("it-IT", "Spiegare il motivo della restituzione")]));
            migration.InsertObjective($"{variant2Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Informar a data e os detalhes da compra para verificação"), new("en-US", "Give the purchase date and details for verification"), new("fr-FR", "Indiquer la date et les d?tails de l'achat pour v?rification"), new("it-IT", "Fornire data e dettagli dell'acquisto per la verifica")]));
            migration.InsertObjective($"{variant2Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Entender as opções: trocar ou receber o valor de volta"), new("en-US", "Understand the options: exchange or refund"), new("fr-FR", "Comprendre les options : ?change ou remboursement"), new("it-IT", "Capire le opzioni: cambio o rimborso")]));
            migration.InsertObjective($"{variant2Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Confirmar se o reembolso vai para o cartão ou em dinheiro"), new("en-US", "Confirm whether the refund goes to the card or in cash"), new("fr-FR", "Confirmer si le remboursement se fait sur la carte ou en esp?ces"), new("it-IT", "Confermare se il rimborso va sulla carta o in contanti")]));

            // Outlet
            const string situacao2Slug = "outlet";
            migration.InsertSituation(situacao2Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Outlet e liquidação", "Entenda promoções e cupons antes de pagar mais do que precisava."), new("en-US", "Outlet and sale", "Understand promotions and coupons before paying more than you needed to."), new("fr-FR", "Outlet et soldes", "Comprenez les promotions et les coupons avant de payer plus que n?cessaire."), new("it-IT", "Outlet e saldi", "Capisci promozioni e coupon prima di pagare pi? del necessario.")]
            ));

            var situacao2Id = ContentId.From(situacao2Slug);

            // Outlet - Desconto
            const string variant3Slug = "outlet-desconto";
            migration.InsertSituationVariant(variant3Slug, new SituationVariant(
                situationId: situacao2Id,
                learningLanguage: "en",
                promptInstructions: """You are a sales associate in an outlet store. You are knowledgeable about promotions and pricing. When a customer asks about sale signs or discounts, explain what they mean: "Buy one get one free," "50% off," "Clearance," "Final sale," etc. Clarify whether discounts are already applied or work at checkout, if coupons can stack, and about the return policy for sale items. Answer questions honestly and help them navigate the store promotions. Be enthusiastic about good deals.""",
                initialMessage: "Hi! Looking for a great deal? We have some amazing discounts today. Do you have any questions about our promotions?",
                translations: [new("pt-BR", "Entendendo promoções", "Você está em um outlet e vê várias placas de promoção com termos em inglês. Um vendedor está por perto."), new("en-US", "Understanding promotions", "You are at an outlet and see several promotion signs with terms in English. A salesperson is nearby."), new("fr-FR", "Comprendre les promotions", "Vous ?tes dans un outlet et voyez plusieurs panneaux promotionnels avec des termes en anglais. Un vendeur est ? proximit?."), new("it-IT", "Capire le promozioni", "Sei in un outlet e vedi vari cartelli promozionali con termini in inglese. Un commesso ? vicino.")]
            ));

            var variant3Id = ContentId.From(variant3Slug);
            migration.InsertObjective($"{variant3Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Perguntar o que significa 'buy one get one free'"), new("en-US", "Ask what 'buy one get one free' means"), new("fr-FR", "Demander ce que signifie 'buy one get one free'"), new("it-IT", "Chiedere cosa significa 'buy one get one free'")]));
            migration.InsertObjective($"{variant3Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Confirmar se o desconto já está aplicado no preço"), new("en-US", "Confirm whether the discount is already applied to the price"), new("fr-FR", "Confirmer si la r?duction est d?j? appliqu?e au prix"), new("it-IT", "Confermare se lo sconto ? gi? applicato al prezzo")]));
            migration.InsertObjective($"{variant3Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Perguntar se há desconto adicional para turistas"), new("en-US", "Ask if there is an additional discount for tourists"), new("fr-FR", "Demander s'il existe une r?duction suppl?mentaire pour les touristes"), new("it-IT", "Chiedere se c'? uno sconto aggiuntivo per turisti")]));
            migration.InsertObjective($"{variant3Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Informar ao caixa que você tem um cupom de desconto e como aplicá-lo"), new("en-US", "Tell the cashier that you have a discount coupon and how to apply it"), new("fr-FR", "Dire au caissier que vous avez un coupon de r?duction et comment l'appliquer"), new("it-IT", "Dire al cassiere che hai un buono sconto e come applicarlo")]));
        }

        private static void SeedEmergenciasCategory(MigrationBuilder migration)
        {
            const string categorySlug = "emergencias";
            migration.InsertCategory(categorySlug, new Category(
                icon: "error-outline",
                translations: [new("pt-BR", "Emergências"), new("en-US", "Emergencies"), new("fr-FR", "Urgences"), new("it-IT", "Emergenze")]
            ));

            var categoryId = ContentId.From(categorySlug);

            // Farmácia
            const string situacao1Slug = "farmacia";
            migration.InsertSituation(situacao1Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Na farmácia", "Descreva sintomas e entenda a dosagem para não comprar o remédio errado."), new("en-US", "At the pharmacy", "Describe symptoms and understand dosage so you do not buy the wrong medicine."), new("fr-FR", "? la pharmacie", "D?crivez les sympt?mes et comprenez la posologie pour ne pas acheter le mauvais m?dicament."), new("it-IT", "In farmacia", "Descrivi i sintomi e capisci il dosaggio per non comprare il medicinale sbagliato.")]
            ));

            var situacao1Id = ContentId.From(situacao1Slug);

            // Farmácia - Dor
            const string variant1Slug = "farmacia-dor";
            migration.InsertSituationVariant(variant1Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are a pharmacy technician helping a customer with over-the-counter pain relief. You are knowledgeable, empathetic, and professional. Listen to their symptoms (headache, fever, body aches, etc.), ask follow-up questions (duration, severity, allergies), recommend an appropriate pain reliever (ibuprofen, acetaminophen, aspirin), explain dosage, frequency, and side effects. Answer questions about effectiveness and timing. Warn about interactions if they mention other medications. Speak clearly and at a calm pace.""",
                initialMessage: "Hi, welcome. I see you're here for pain relief. What's bothering you today?",
                translations: [new("pt-BR", "Dor de cabeça e febre", "Você acorda mal no segundo dia de viagem. Vai à farmácia próxima ao hotel para comprar um analgésico sem receita."), new("en-US", "Headache and fever", "You wake up feeling sick on the second day of the trip. You go to the pharmacy near the hotel to buy an over-the-counter pain reliever."), new("fr-FR", "Mal de t?te et fi?vre", "Vous vous r?veillez malade le deuxi?me jour du voyage. Vous allez ? la pharmacie pr?s de l'h?tel pour acheter un antidouleur sans ordonnance."), new("it-IT", "Mal di testa e febbre", "Ti svegli male il secondo giorno di viaggio. Vai alla farmacia vicino all'hotel per comprare un analgesico da banco.")]
            ));

            var variant1Id = ContentId.From(variant1Slug);
            migration.InsertObjective($"{variant1Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Descrever os sintomas ao atendente"), new("en-US", "Describe the symptoms to the attendant"), new("fr-FR", "D?crire les sympt?mes au pharmacien"), new("it-IT", "Descrivere i sintomi all'addetto")]));
            migration.InsertObjective($"{variant1Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Perguntar qual medicamento é recomendado"), new("en-US", "Ask which medication is recommended"), new("fr-FR", "Demander quel m?dicament est recommand?"), new("it-IT", "Chiedere quale medicinale ? consigliato")]));
            migration.InsertObjective($"{variant1Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Perguntar sobre dosagem e frequência"), new("en-US", "Ask about dosage and frequency"), new("fr-FR", "Demander la posologie et la fr?quence"), new("it-IT", "Chiedere dosaggio e frequenza")]));
            migration.InsertObjective($"{variant1Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Perguntar se precisa de receita"), new("en-US", "Ask if a prescription is needed"), new("fr-FR", "Demander s'il faut une ordonnance"), new("it-IT", "Chiedere se serve una ricetta")]));
            migration.InsertObjective($"{variant1Slug}:obj:5", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Confirmar a compra e verificar a dosagem recomendada antes de sair"), new("en-US", "Confirm the purchase and check the recommended dosage before leaving"), new("fr-FR", "Confirmer l'achat et v?rifier la posologie recommand?e avant de partir"), new("it-IT", "Confermare l'acquisto e verificare il dosaggio consigliato prima di uscire")]));

            // Farmácia - Alergia
            const string variant2Slug = "farmacia-alergia";
            migration.InsertSituationVariant(variant2Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are a pharmacy technician assisting a customer with a mild allergic reaction. You are calm, reassuring, and careful. Ask detailed questions about the reaction (where, severity, when it started), what they ate or touched, and previous allergies. Ask if they have trouble breathing or swelling in the throat (red flags for urgent care). Recommend an antihistamine and explain how it works. Advise when to seek medical attention. Be cautious and suggest seeing a doctor if the reaction seems more serious.""",
                initialMessage: "Hi there. I see you're having an allergic reaction. Can you describe what's happening?",
                translations: [new("pt-BR", "Reação alérgica leve", "Você teve uma reação alérgica leve após comer algo. A pele está coçando e levemente inchada. Vai à farmácia antes de considerar médico."), new("en-US", "Mild allergic reaction", "You had a mild allergic reaction after eating something. Your skin is itchy and slightly swollen. You go to the pharmacy before considering a doctor."), new("fr-FR", "R?action allergique l?g?re", "Vous avez eu une r?action allergique l?g?re apr?s avoir mang? quelque chose. Votre peau d?mange et est l?g?rement gonfl?e. Vous allez ? la pharmacie avant d'envisager un m?decin."), new("it-IT", "Reazione allergica lieve", "Hai avuto una lieve reazione allergica dopo aver mangiato qualcosa. La pelle prude ed ? leggermente gonfia. Vai in farmacia prima di considerare un medico.")]
            ));

            var variant2Id = ContentId.From(variant2Slug);
            migration.InsertObjective($"{variant2Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Descrever a reação e onde ela está no corpo"), new("en-US", "Describe the reaction and where it is on the body"), new("fr-FR", "D?crire la r?action et o? elle se trouve sur le corps"), new("it-IT", "Descrivere la reazione e dove si trova sul corpo")]));
            migration.InsertObjective($"{variant2Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Explicar o que você comeu ou tocou"), new("en-US", "Explain what you ate or touched"), new("fr-FR", "Expliquer ce que vous avez mang? ou touch?"), new("it-IT", "Spiegare cosa hai mangiato o toccato")]));
            migration.InsertObjective($"{variant2Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Perguntar por um anti-histamínico"), new("en-US", "Ask for an antihistamine"), new("fr-FR", "Demander un antihistaminique"), new("it-IT", "Chiedere un antistaminico")]));
            migration.InsertObjective($"{variant2Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Entender se você deve procurar um médico"), new("en-US", "Understand whether you should see a doctor"), new("fr-FR", "Comprendre si vous devez consulter un m?decin"), new("it-IT", "Capire se devi consultare un medico")]));
            migration.InsertObjective($"{variant2Slug}:obj:5", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Confirmar a compra do produto recomendado pelo atendente"), new("en-US", "Confirm the purchase of the product recommended by the attendant"), new("fr-FR", "Confirmer l'achat du produit recommand? par le pharmacien"), new("it-IT", "Confermare l'acquisto del prodotto consigliato dall'addetto")]));

            // Pronto-socorro
            const string situacao2Slug = "pronto-socorro";
            migration.InsertSituation(situacao2Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Pronto-socorro", "Explique sintomas, seguro e alergias quando um erro pode atrasar seu atendimento."), new("en-US", "Emergency room", "Explain symptoms, insurance, and allergies when a mistake could delay your care."), new("fr-FR", "Urgences m?dicales", "Expliquez les sympt?mes, l'assurance et les allergies lorsqu'une erreur pourrait retarder vos soins."), new("it-IT", "Pronto soccorso", "Spiega sintomi, assicurazione e allergie quando un errore potrebbe ritardare l'assistenza.")]
            ));

            var situacao2Id = ContentId.From(situacao2Slug);

            // Pronto-socorro - Queda
            const string variant3Slug = "pronto-socorro-queda";
            migration.InsertSituationVariant(variant3Slug, new SituationVariant(
                situationId: situacao2Id,
                learningLanguage: "en",
                promptInstructions: """You are an emergency room receptionist triaging an injured patient. You are professional, calm, and efficient. Ask what happened (how the injury occurred, when), where the pain is, and severity (1-10 pain scale). Ask about medical history, allergies, current medications, and travel insurance. Explain the intake process. A nurse or doctor will take them back shortly. Be empathetic to their pain and worry. Speak clearly and ask patients to wait for the next available provider.""",
                initialMessage: "Welcome to the ER. I can see you're in pain. Let me get some information from you. How did this happen?",
                translations: [new("pt-BR", "Dor intensa e possível fratura", "Você caiu em um passeio e está com o tornozelo muito inchado. Um amigo te leva ao pronto-socorro mais próximo."), new("en-US", "Severe pain and possible fracture", "You fell during an outing and your ankle is very swollen. A friend takes you to the nearest emergency room."), new("fr-FR", "Douleur intense et possible fracture", "Vous ?tes tomb? pendant une sortie et votre cheville est tr?s enfl?e. Un ami vous emm?ne aux urgences les plus proches."), new("it-IT", "Dolore intenso e possibile frattura", "Sei caduto durante una gita e la caviglia ? molto gonfia. Un amico ti porta al pronto soccorso pi? vicino.")]
            ));

            var variant3Id = ContentId.From(variant3Slug);
            migration.InsertObjective($"{variant3Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Explicar o que aconteceu na recepção"), new("en-US", "Explain what happened at reception"), new("fr-FR", "Expliquer ce qui s'est pass? ? l'accueil"), new("it-IT", "Spiegare cosa ? successo all'accettazione")]));
            migration.InsertObjective($"{variant3Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Descrever a localização e nível da dor"), new("en-US", "Describe the location and level of pain"), new("fr-FR", "D?crire l'emplacement et le niveau de la douleur"), new("it-IT", "Descrivere posizione e livello del dolore")]));
            migration.InsertObjective($"{variant3Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Informar se tem seguro viagem"), new("en-US", "Say whether you have travel insurance"), new("fr-FR", "Indiquer si vous avez une assurance voyage"), new("it-IT", "Dire se hai un'assicurazione di viaggio")]));
            migration.InsertObjective($"{variant3Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Responder às perguntas do médico sobre alergias e medicamentos"), new("en-US", "Answer the doctor's questions about allergies and medications"), new("fr-FR", "R?pondre aux questions du m?decin sur les allergies et les m?dicaments"), new("it-IT", "Rispondere alle domande del medico su allergie e farmaci")]));
            migration.InsertObjective($"{variant3Slug}:obj:5", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Entender as instruções médicas ao sair"), new("en-US", "Understand the medical instructions when leaving"), new("fr-FR", "Comprendre les consignes m?dicales en sortant"), new("it-IT", "Capire le istruzioni mediche all'uscita")]));

            // Polícia
            const string situacao3Slug = "policia";
            migration.InsertSituation(situacao3Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Abordagem policial ou ocorrência", "Registre o ocorrido com dados claros para evitar problemas com seguro ou documentos."), new("en-US", "Police approach or report", "Record what happened with clear details to avoid problems with insurance or documents."), new("fr-FR", "Contr?le de police ou d?claration", "D?clarez ce qui s'est pass? avec des donn?es claires pour ?viter des probl?mes avec l'assurance ou les documents."), new("it-IT", "Controllo di polizia o denuncia", "Registra l'accaduto con dati chiari per evitare problemi con assicurazione o documenti.")]
            ));

            var situacao3Id = ContentId.From(situacao3Slug);

            // Polícia - Roubo
            const string variant4Slug = "policia-roubo";
            migration.InsertSituationVariant(variant4Slug, new SituationVariant(
                situationId: situacao3Id,
                learningLanguage: "en",
                promptInstructions: """You are a police officer taking a theft report at the station. You are professional, sympathetic, and detail-oriented. Ask the victim what was stolen, when and where it happened, and circumstances (crowded area, pickpocket, etc.). Ask for descriptions of the stolen items. Gather personal information (name, passport number, phone). Explain the report process and that a copy will be provided for insurance claims. Ask if they need any other assistance or resources. Be straightforward and efficient.""",
                initialMessage: "Good afternoon. I understand you've been robbed. I'll file a report for you. Can you tell me what happened?",
                translations: [new("pt-BR", "Registrando um furto", "Sua carteira foi furtada em um ponto turístico. Você precisa registrar o boletim de ocorrência para acionar o seguro."), new("en-US", "Reporting a theft", "Your wallet was stolen at a tourist spot. You need to file a police report to activate the insurance."), new("fr-FR", "D?clarer un vol", "Votre portefeuille a ?t? vol? dans un lieu touristique. Vous devez d?poser une plainte pour faire jouer l'assurance."), new("it-IT", "Denunciare un furto", "Ti hanno rubato il portafoglio in un luogo turistico. Devi fare denuncia per attivare l'assicurazione.")]
            ));

            var variant4Id = ContentId.From(variant4Slug);
            migration.InsertObjective($"{variant4Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Explicar o que aconteceu e onde"), new("en-US", "Explain what happened and where"), new("fr-FR", "Expliquer ce qui s'est pass? et o?"), new("it-IT", "Spiegare cosa ? successo e dove")]));
            migration.InsertObjective($"{variant4Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Descrever os itens que foram levados"), new("en-US", "Describe the items that were taken"), new("fr-FR", "D?crire les objets qui ont ?t? emport?s"), new("it-IT", "Descrivere gli oggetti rubati")]));
            migration.InsertObjective($"{variant4Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Fornecer seus dados pessoais ao policial"), new("en-US", "Provide your personal details to the police officer"), new("fr-FR", "Fournir vos informations personnelles au policier"), new("it-IT", "Fornire i tuoi dati personali al poliziotto")]));
            migration.InsertObjective($"{variant4Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Perguntar como obter uma cópia do boletim"), new("en-US", "Ask how to get a copy of the report"), new("fr-FR", "Demander comment obtenir une copie du proc?s-verbal"), new("it-IT", "Chiedere come ottenere una copia della denuncia")]));

            // Cartão bloqueado
            const string situacao4Slug = "cartao-bloqueado";
            migration.InsertSituation(situacao4Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Cartão bloqueado ou problema bancário", "Resolva bloqueios e verificações antes que uma compra recusada vire um problema maior."), new("en-US", "Blocked card or banking problem", "Resolve blocks and verifications before a declined purchase becomes a bigger problem."), new("fr-FR", "Carte bloqu?e ou probl?me bancaire", "R?solvez les blocages et v?rifications avant qu'un achat refus? ne devienne un probl?me plus grave."), new("it-IT", "Carta bloccata o problema bancario", "Risolvi blocchi e verifiche prima che un acquisto rifiutato diventi un problema maggiore.")]
            ));

            var situacao4Id = ContentId.From(situacao4Slug);

            // Cartão - Ligação
            const string variant5Slug = "cartao-ligacao";
            migration.InsertSituationVariant(variant5Slug, new SituationVariant(
                situationId: situacao4Id,
                learningLanguage: "en",
                promptInstructions: """You are an international credit card support agent. You are professional and solution-focused. Listen to the customer explain their issue (card declined, traveling abroad, needs to unblock). Ask for card number (last 4 digits), type of transaction, and location. Guide them through security verification questions (name, address, recent transactions). Explain why the card was declined if applicable. Work toward a solution: unblock for international use, replace card at a branch, or offer alternatives. Be efficient and reassuring.""",
                initialMessage: "Thank you for calling international card support. I see you're calling from abroad. How can I help you today?",
                translations: [new("pt-BR", "Ligando para o suporte internacional do cartão", "Seu cartão foi recusado em uma loja dos EUA. Você vira o cartão e encontra o número de suporte internacional — o atendimento é em inglês. Você precisa resolver isso rapidamente."), new("en-US", "Calling international card support", "Your card was declined in a store in the U.S. You turn the card over and find the international support number; service is in English. You need to resolve this quickly."), new("fr-FR", "Appeler l'assistance internationale de la carte", "Votre carte a ?t? refus?e dans un magasin aux ?tats-Unis. Vous retournez la carte et trouvez le num?ro d'assistance internationale ; le service est en anglais. Vous devez r?soudre cela rapidement."), new("it-IT", "Chiamare il supporto internazionale della carta", "La tua carta ? stata rifiutata in un negozio negli Stati Uniti. Giri la carta e trovi il numero di supporto internazionale; l'assistenza ? in inglese. Devi risolvere rapidamente.")]
            ));

            var variant5Id = ContentId.From(variant5Slug);
            migration.InsertObjective($"{variant5Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant5Id, translations: [new("pt-BR", "Explicar ao atendente que está viajando no exterior e que o cartão foi recusado"), new("en-US", "Explain to the agent that you are traveling abroad and that the card was declined"), new("fr-FR", "Expliquer au conseiller que vous voyagez ? l'?tranger et que la carte a ?t? refus?e"), new("it-IT", "Spiegare all'operatore che sei in viaggio all'estero e che la carta ? stata rifiutata")]));
            migration.InsertObjective($"{variant5Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant5Id, translations: [new("pt-BR", "Passar pelas etapas de verificação de identidade em inglês"), new("en-US", "Go through the identity verification steps in English"), new("fr-FR", "Passer les ?tapes de v?rification d'identit? en anglais"), new("it-IT", "Superare le fasi di verifica dell'identit? in inglese")]));
            migration.InsertObjective($"{variant5Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant5Id, translations: [new("pt-BR", "Solicitar o desbloqueio do cartão para uso internacional"), new("en-US", "Request the card to be unblocked for international use"), new("fr-FR", "Demander le d?blocage de la carte pour une utilisation internationale"), new("it-IT", "Richiedere lo sblocco della carta per l'uso internazionale")]));
            migration.InsertObjective($"{variant5Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant5Id, translations: [new("pt-BR", "Confirmar que o cartão foi liberado e se pode tentar a compra novamente"), new("en-US", "Confirm that the card has been released and whether you can try the purchase again"), new("fr-FR", "Confirmer que la carte a ?t? d?bloqu?e et si vous pouvez r?essayer l'achat"), new("it-IT", "Confermare che la carta ? stata sbloccata e se puoi riprovare l'acquisto")]));
        }

        private static void SeedTurismoCategory(MigrationBuilder migration)
        {
            const string categorySlug = "turismo";
            migration.InsertCategory(categorySlug, new Category(
                icon: "map",
                translations: [new("pt-BR", "Turismo & lazer"), new("en-US", "Tourism & leisure"), new("fr-FR", "Tourisme et loisirs"), new("it-IT", "Turismo e tempo libero")]
            ));

            var categoryId = ContentId.From(categorySlug);

            // Ingressos
            const string situacao1Slug = "ingressos";
            migration.InsertSituation(situacao1Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Comprando ingressos", "Compre o ingresso certo e entenda regras antes de perder horário, desconto ou entrada."), new("en-US", "Buying tickets", "Buy the right ticket and understand rules before missing a time slot, discount, or entry."), new("fr-FR", "Acheter des billets", "Achetez le bon billet et comprenez les r?gles avant de manquer un horaire, une r?duction ou l'entr?e."), new("it-IT", "Comprare biglietti", "Compra il biglietto giusto e capisci le regole prima di perdere orario, sconto o ingresso.")]
            ));

            var situacao1Id = ContentId.From(situacao1Slug);

            // Ingressos - Parque
            const string variant1Slug = "ingressos-parque";
            migration.InsertSituationVariant(variant1Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are a ticket booth agent at a theme park. You are friendly and efficient. Greet the customer and ask how many adults and children need tickets. Explain ticket options (1-day, multi-day, park hopper). Ask about discounts (annual pass, AAA, students, military). Calculate prices and show the total clearly. Explain what's included with each ticket type. Answer questions about hours, attractions, and park rules. Process payment and issue tickets. Be helpful and enthusiastic about their visit.""",
                initialMessage: "Welcome to [Park]! How many people will we be getting tickets for today?",
                translations: [new("pt-BR", "Parque temático", "Você está na bilheteria de um parque temático e precisa comprar ingressos para um grupo com adultos e crianças."), new("en-US", "Theme park", "You are at the ticket office of a theme park and need to buy tickets for a group with adults and children."), new("fr-FR", "Parc ? th?me", "Vous ?tes ? la billetterie d'un parc ? th?me et devez acheter des billets pour un groupe avec adultes et enfants."), new("it-IT", "Parco a tema", "Sei alla biglietteria di un parco a tema e devi comprare biglietti per un gruppo con adulti e bambini.")]
            ));

            var variant1Id = ContentId.From(variant1Slug);
            migration.InsertObjective($"{variant1Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Informar a quantidade de adultos e crianças"), new("en-US", "State the number of adults and children"), new("fr-FR", "Indiquer le nombre d'adultes et d'enfants"), new("it-IT", "Indicare il numero di adulti e bambini")]));
            migration.InsertObjective($"{variant1Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Perguntar sobre desconto para grupos ou estudantes"), new("en-US", "Ask about group or student discounts"), new("fr-FR", "Demander les r?ductions pour groupes ou ?tudiants"), new("it-IT", "Chiedere sconti per gruppi o studenti")]));
            migration.InsertObjective($"{variant1Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Entender as opções de ingresso (1 dia, 2 dias, hopper)"), new("en-US", "Understand the ticket options (1 day, 2 days, hopper)"), new("fr-FR", "Comprendre les options de billet (1 jour, 2 jours, hopper)"), new("it-IT", "Capire le opzioni di biglietto (1 giorno, 2 giorni, hopper)")]));
            migration.InsertObjective($"{variant1Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Verificar e confirmar os ingressos antes de finalizar a compra"), new("en-US", "Check and confirm the tickets before completing the purchase"), new("fr-FR", "V?rifier et confirmer les billets avant de finaliser l'achat"), new("it-IT", "Verificare e confermare i biglietti prima di finalizzare l'acquisto")]));

            // Ingressos - Museu
            const string variant2Slug = "ingressos-museu";
            migration.InsertSituationVariant(variant2Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are a museum ticket and information agent. You are knowledgeable and welcoming. Sell admission tickets and explain what's included. Tell them about guided tour options, languages available, start times, and length of tours. Describe what's in the museum (exhibits, highlights). Ask if they're interested in a guided tour and help book it. Answer questions about photography policies, museum hours, amenities (restrooms, café), and accessibility. Be helpful and encourage them to enjoy their visit.""",
                initialMessage: "Good morning! Welcome to [Museum]. Would you like just an admission ticket, or are you interested in a guided tour?",
                translations: [new("pt-BR", "Museu", "Você visita um museu e além do ingresso, quer participar de uma visita guiada no idioma local."), new("en-US", "Museum", "You visit a museum and, besides the ticket, want to join a guided tour in the local language."), new("fr-FR", "Mus?e", "Vous visitez un mus?e et, en plus du billet, souhaitez participer ? une visite guid?e dans la langue locale."), new("it-IT", "Museo", "Visiti un museo e, oltre al biglietto, vuoi partecipare a una visita guidata nella lingua locale.")]
            ));

            var variant2Id = ContentId.From(variant2Slug);
            migration.InsertObjective($"{variant2Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Solicitar o ingresso ao atendente e confirmar as opções disponíveis"), new("en-US", "Request the ticket from the attendant and confirm the available options"), new("fr-FR", "Demander le billet au guichetier et confirmer les options disponibles"), new("it-IT", "Richiedere il biglietto all'addetto e confermare le opzioni disponibili")]));
            migration.InsertObjective($"{variant2Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Perguntar sobre o horário da próxima visita guiada"), new("en-US", "Ask about the time of the next guided tour"), new("fr-FR", "Demander l'heure de la prochaine visite guid?e"), new("it-IT", "Chiedere l'orario della prossima visita guidata")]));
            migration.InsertObjective($"{variant2Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Entender a duração e rota do tour"), new("en-US", "Understand the duration and route of the tour"), new("fr-FR", "Comprendre la dur?e et le parcours de la visite"), new("it-IT", "Capire durata e percorso del tour")]));
            migration.InsertObjective($"{variant2Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Perguntar se é permitido fotografar"), new("en-US", "Ask whether photography is allowed"), new("fr-FR", "Demander s'il est permis de prendre des photos"), new("it-IT", "Chiedere se ? permesso fotografare")]));

            // Pedindo direções
            const string situacao2Slug = "pedindo-direcoes";
            migration.InsertSituation(situacao2Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Pedindo direções na rua", "Peça e confirme o caminho para não depender só do GPS quando ele falhar."), new("en-US", "Asking for directions on the street", "Ask for and confirm the way so you do not depend only on GPS when it fails."), new("fr-FR", "Demander son chemin dans la rue", "Demandez et confirmez le chemin pour ne pas d?pendre seulement du GPS lorsqu'il ?choue."), new("it-IT", "Chiedere indicazioni per strada", "Chiedi e conferma il percorso per non dipendere solo dal GPS quando non funziona.")]
            ));

            var situacao2Id = ContentId.From(situacao2Slug);

            // Direções - Caminhando
            const string variant3Slug = "direcoes-caminhando";
            migration.InsertSituationVariant(variant3Slug, new SituationVariant(
                situationId: situacao2Id,
                learningLanguage: "en",
                promptInstructions: """You are a local pedestrian approached for directions. You are friendly and helpful. Listen to where they want to go. Provide clear directions using landmarks, street names, and distance (e.g., "About 5 blocks," "Turn left at the coffee shop"). Suggest alternatives if the route is complicated (taxi, bus, walking app). Be encouraging and ask if they need clarification. Speak at a natural pace, not too fast. Wish them well as they leave.""",
                initialMessage: "Hi! Are you looking for something? I can help if you're lost.",
                translations: [new("pt-BR", "A pé em área turística", "Você está perdido próximo a uma atração turística e aborda um transeunte para perguntar o caminho."), new("en-US", "On foot in a tourist area", "You are lost near a tourist attraction and approach a passerby to ask for directions."), new("fr-FR", "? pied dans une zone touristique", "Vous ?tes perdu pr?s d'une attraction touristique et abordez un passant pour demander votre chemin."), new("it-IT", "A piedi in una zona turistica", "Ti sei perso vicino a un'attrazione turistica e fermi un passante per chiedere indicazioni.")]
            ));

            var variant3Id = ContentId.From(variant3Slug);
            migration.InsertObjective($"{variant3Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Pedir direções de forma educada"), new("en-US", "Ask for directions politely"), new("fr-FR", "Demander son chemin poliment"), new("it-IT", "Chiedere indicazioni in modo educato")]));
            migration.InsertObjective($"{variant3Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Entender referências de direção e distância dadas pelo transeunte"), new("en-US", "Understand direction and distance references given by the passerby"), new("fr-FR", "Comprendre les indications de direction et de distance donn?es par le passant"), new("it-IT", "Capire riferimenti di direzione e distanza dati dal passante")]));
            migration.InsertObjective($"{variant3Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Pedir para repetir se não entender"), new("en-US", "Ask them to repeat if you do not understand"), new("fr-FR", "Demander de r?p?ter si vous ne comprenez pas"), new("it-IT", "Chiedere di ripetere se non capisci")]));
            migration.InsertObjective($"{variant3Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Confirmar que entendeu o caminho"), new("en-US", "Confirm that you understood the way"), new("fr-FR", "Confirmer que vous avez compris le chemin"), new("it-IT", "Confermare di aver capito il percorso")]));
        }

        private static void SeedDiaADiaCategory(MigrationBuilder migration)
        {
            const string categorySlug = "dia-a-dia";
            migration.InsertCategory(categorySlug, new Category(
                icon: "mood",
                translations: [new("pt-BR", "Dia a dia"), new("en-US", "Daily life"), new("fr-FR", "Vie quotidienne"), new("it-IT", "Vita quotidiana")]
            ));

            var categoryId = ContentId.From(categorySlug);

            // Small talk
            const string situacao1Slug = "smalltalk";
            migration.InsertSituation(situacao1Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Small talk com nativos", "Responda conversas casuais sem parecer ríspido ou encerrar a interação sem querer."), new("en-US", "Small talk with native speakers", "Respond to casual conversations without seeming rude or accidentally ending the interaction."), new("fr-FR", "Petite conversation avec des locuteurs natifs", "R?pondez aux conversations informelles sans para?tre brusque ni mettre fin ? l'interaction sans le vouloir."), new("it-IT", "Conversazione informale con madrelingua", "Rispondi a conversazioni casuali senza sembrare brusco o chiudere l'interazione senza volerlo.")]
            ));

            var situacao1Id = ContentId.From(situacao1Slug);

            // Small talk - Elevador
            const string variant1Slug = "smalltalk-elevador";
            migration.InsertSituationVariant(variant1Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are another hotel guest making casual small talk in an elevator. You are friendly and relaxed. Comment on something neutral (weather, the hotel, city). Ask where they're from or if they're visiting. Keep the conversation light and natural. Don't ask overly personal questions. Respond to their answers and keep the exchange brief since you'll get off at a different floor soon. Be warm and end the conversation naturally when your floor comes up.""",
                initialMessage: "Hey, good morning! Nice weather today, huh? Are you visiting, or do you live here?",
                translations: [new("pt-BR", "Conversa no elevador do hotel", "Outro hóspede entra no elevador e inicia uma conversa casual sobre o tempo e sua origem."), new("en-US", "Conversation in the hotel elevator", "Another guest enters the elevator and starts a casual conversation about the weather and where you are from."), new("fr-FR", "Conversation dans l'ascenseur de l'h?tel", "Un autre client entre dans l'ascenseur et commence une conversation informelle sur le temps et votre origine."), new("it-IT", "Conversazione nell'ascensore dell'hotel", "Un altro ospite entra in ascensore e inizia una conversazione informale sul tempo e sulla tua origine.")]
            ));

            var variant1Id = ContentId.From(variant1Slug);
            migration.InsertObjective($"{variant1Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Responder a uma saudação casual de forma natural"), new("en-US", "Respond to a casual greeting naturally"), new("fr-FR", "R?pondre naturellement ? une salutation informelle"), new("it-IT", "Rispondere in modo naturale a un saluto informale")]));
            migration.InsertObjective($"{variant1Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Falar de onde você é e por que veio"), new("en-US", "Say where you are from and why you came"), new("fr-FR", "Dire d'o? vous venez et pourquoi vous ?tes venu"), new("it-IT", "Dire da dove vieni e perch? sei venuto")]));
            migration.InsertObjective($"{variant1Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Comentar sobre o clima ou a cidade"), new("en-US", "Comment on the weather or the city"), new("fr-FR", "Commenter le temps ou la ville"), new("it-IT", "Commentare il tempo o la citt?")]));
            migration.InsertObjective($"{variant1Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant1Id, translations: [new("pt-BR", "Encerrar a conversa de forma educada ao chegar no seu andar"), new("en-US", "End the conversation politely when you reach your floor"), new("fr-FR", "Terminer la conversation poliment en arrivant ? votre ?tage"), new("it-IT", "Chiudere la conversazione educatamente quando arrivi al tuo piano")]));

            // Small talk - Fila
            const string variant2Slug = "smalltalk-fila";
            migration.InsertSituationVariant(variant2Slug, new SituationVariant(
                situationId: situacao1Id,
                learningLanguage: "en",
                promptInstructions: """You are someone in a long theme park line making conversation. You are casual and friendly. Comment on the wait time, the ride/attraction ahead, or the park. Ask if it's their first time or if they've been on this ride before. Share your own experience or opinion. Use informal language. Be conversational and relaxed. Respond to their comments and keep the chat going until you board the ride.""",
                initialMessage: "Man, this line is crazy, huh? Have you been on this ride before, or is this your first time?",
                translations: [new("pt-BR", "Papo na fila", "Você está em uma fila longa em um parque e a pessoa ao lado começa a conversar sobre o tempo de espera e os brinquedos."), new("en-US", "Chat in line", "You are in a long line at a park and the person next to you starts talking about the wait time and the rides."), new("fr-FR", "Discussion dans la file", "Vous ?tes dans une longue file dans un parc et la personne ? c?t? commence ? parler du temps d'attente et des attractions."), new("it-IT", "Chiacchierata in fila", "Sei in una lunga fila in un parco e la persona accanto inizia a parlare del tempo di attesa e delle attrazioni.")]
            ));

            var variant2Id = ContentId.From(variant2Slug);
            migration.InsertObjective($"{variant2Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Responder ao comentário inicial da pessoa sobre a fila ou a atração de forma natural"), new("en-US", "Respond naturally to the person's opening comment about the line or attraction"), new("fr-FR", "R?pondre naturellement au commentaire initial de la personne sur la file ou l'attraction"), new("it-IT", "Rispondere naturalmente al commento iniziale della persona sulla fila o sull'attrazione")]));
            migration.InsertObjective($"{variant2Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Compartilhar sua opinião sobre a atração"), new("en-US", "Share your opinion about the attraction"), new("fr-FR", "Partager votre avis sur l'attraction"), new("it-IT", "Condividere la tua opinione sull'attrazione")]));
            migration.InsertObjective($"{variant2Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Perguntar de onde a pessoa é"), new("en-US", "Ask where the person is from"), new("fr-FR", "Demander d'o? vient la personne"), new("it-IT", "Chiedere da dove viene la persona")]));
            migration.InsertObjective($"{variant2Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant2Id, translations: [new("pt-BR", "Usar expressões informais típicas do idioma"), new("en-US", "Use informal expressions typical of the language"), new("fr-FR", "Utiliser des expressions informelles typiques de la langue"), new("it-IT", "Usare espressioni informali tipiche della lingua")]));

            // Pedir repetir
            const string situacao2Slug = "pedir-repetir";
            migration.InsertSituation(situacao2Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Não entendi — pede pra repetir", "Peça repetição do jeito certo para não seguir instruções que você não entendeu."), new("en-US", "I did not understand - ask them to repeat", "Ask for repetition the right way so you do not follow instructions you did not understand."), new("fr-FR", "Je n'ai pas compris - demander de r?p?ter", "Demandez de r?p?ter correctement pour ne pas suivre des instructions que vous n'avez pas comprises."), new("it-IT", "Non ho capito - chiedere di ripetere", "Chiedi di ripetere nel modo giusto per non seguire istruzioni che non hai capito.")]
            ));

            var situacao2Id = ContentId.From(situacao2Slug);

            // Repetir - Rápido
            const string variant3Slug = "repetir-rapido";
            migration.InsertSituationVariant(variant3Slug, new SituationVariant(
                situationId: situacao2Id,
                learningLanguage: "en",
                promptInstructions: """You are a busy native speaker who just gave rapid instructions. The person asks you to repeat or slow down. Accommodate them gracefully—repeat what you said, speak more slowly and clearly, and break it into smaller chunks. Simplify your language if needed. Be patient but efficient, as you're in a hurry. Use simpler words or gestures to help them understand. Once they confirm they understand, you can return to normal pace or leave.""",
                initialMessage: "Okay, so you go down this street, turn left at the corner, and the building's right there. Got it?",
                translations: [new("pt-BR", "Nativo falou rápido demais", "Um nativo te deu uma instrução rápida e você não pegou nada. Ele parece na correria mas você precisa entender."), new("en-US", "The native speaker talked too fast", "A native speaker gave you a quick instruction and you did not catch anything. They seem in a hurry, but you need to understand."), new("fr-FR", "Le locuteur natif a parl? trop vite", "Un locuteur natif vous a donn? une instruction rapide et vous n'avez rien saisi. Il semble press?, mais vous devez comprendre."), new("it-IT", "Il madrelingua ha parlato troppo in fretta", "Un madrelingua ti ha dato un'indicazione rapida e non hai capito nulla. Sembra di fretta, ma devi capire.")]
            ));

            var variant3Id = ContentId.From(variant3Slug);
            migration.InsertObjective($"{variant3Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Pedir desculpas e dizer que não entendeu"), new("en-US", "Apologize and say that you did not understand"), new("fr-FR", "S'excuser et dire que vous n'avez pas compris"), new("it-IT", "Scusarsi e dire che non hai capito")]));
            migration.InsertObjective($"{variant3Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Pedir para repetir mais devagar"), new("en-US", "Ask them to repeat more slowly"), new("fr-FR", "Demander de r?p?ter plus lentement"), new("it-IT", "Chiedere di ripetere pi? lentamente")]));
            migration.InsertObjective($"{variant3Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Pedir para falar mais simples se necessário"), new("en-US", "Ask them to speak more simply if necessary"), new("fr-FR", "Demander de parler plus simplement si n?cessaire"), new("it-IT", "Chiedere di parlare in modo pi? semplice se necessario")]));
            migration.InsertObjective($"{variant3Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant3Id, translations: [new("pt-BR", "Confirmar que entendeu ao final"), new("en-US", "Confirm that you understood at the end"), new("fr-FR", "Confirmer que vous avez compris ? la fin"), new("it-IT", "Confermare alla fine di aver capito")]));

            // Foto
            const string situacao3Slug = "foto";
            migration.InsertSituation(situacao3Slug, new Situation(
                categoryId: categoryId,
                translations: [new("pt-BR", "Pedindo foto ou tirando foto para alguém", "Peça ajuda ou oriente a foto sem criar uma situação desconfortável com desconhecidos."), new("en-US", "Asking for a photo or taking a photo for someone", "Ask for help or guide the photo without creating an awkward situation with strangers."), new("fr-FR", "Demander une photo ou prendre une photo pour quelqu'un", "Demandez de l'aide ou guidez la photo sans cr?er une situation g?nante avec des inconnus."), new("it-IT", "Chiedere una foto o fare una foto a qualcuno", "Chiedi aiuto o guida la foto senza creare una situazione imbarazzante con sconosciuti.")]
            ));

            var situacao3Id = ContentId.From(situacao3Slug);

            // Foto - Pedir
            const string variant4Slug = "foto-pedir";
            migration.InsertSituationVariant(variant4Slug, new SituationVariant(
                situationId: situacao3Id,
                learningLanguage: "en",
                promptInstructions: """You are a tourist approached to take someone's photo. You are friendly and helpful. Agree willingly. Take the camera/phone they offer. Ask how many photos they want and if they want to be in a certain spot. Take the photo(s), check that it looks good, and offer to take another if needed. Return the camera/phone and compliment their photo or smile. Be warm and genuine. Wish them well with their trip.""",
                initialMessage: "Hey! Of course, I'd be happy to help! Just hand me your phone.",
                translations: [new("pt-BR", "Pedindo para um desconhecido te fotografar", "Você quer uma foto em frente a um monumento famoso. Aborda um turista próximo para pedir ajuda."), new("en-US", "Asking a stranger to take your photo", "You want a photo in front of a famous monument. You approach a nearby tourist to ask for help."), new("fr-FR", "Demander ? un inconnu de vous prendre en photo", "Vous voulez une photo devant un monument c?l?bre. Vous abordez un touriste ? proximit? pour demander de l'aide."), new("it-IT", "Chiedere a uno sconosciuto di farti una foto", "Vuoi una foto davanti a un monumento famoso. Ti avvicini a un turista vicino per chiedere aiuto.")]
            ));

            var variant4Id = ContentId.From(variant4Slug);
            migration.InsertObjective($"{variant4Slug}:obj:1", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Pedir gentilmente que a pessoa tire a foto"), new("en-US", "Politely ask the person to take the photo"), new("fr-FR", "Demander poliment ? la personne de prendre la photo"), new("it-IT", "Chiedere gentilmente alla persona di scattare la foto")]));
            migration.InsertObjective($"{variant4Slug}:obj:2", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Explicar como usar seu celular se necessário"), new("en-US", "Explain how to use your phone if necessary"), new("fr-FR", "Expliquer comment utiliser votre t?l?phone si n?cessaire"), new("it-IT", "Spiegare come usare il tuo cellulare se necessario")]));
            migration.InsertObjective($"{variant4Slug}:obj:3", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Pedir que tire mais de uma foto"), new("en-US", "Ask them to take more than one photo"), new("fr-FR", "Demander de prendre plus d'une photo"), new("it-IT", "Chiedere di scattare pi? di una foto")]));
            migration.InsertObjective($"{variant4Slug}:obj:4", new SituationVariantObjective(situationVariantId: variant4Id, translations: [new("pt-BR", "Agradecer de forma natural"), new("en-US", "Thank them naturally"), new("fr-FR", "Remercier de fa?on naturelle"), new("it-IT", "Ringraziare in modo naturale")]));
        }
    }
}
