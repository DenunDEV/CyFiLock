🔐 CyFi Lock
======================================================================


- Sistema de Segurança Corporativo - 

📋 Introdução 📋

 CyFi Lock é um sistema de autenticação "corporativo" desenvolvido 
em C# que implementa múltiplas camadas de segurança através de CAPTCHAs 
personalizados e efeitos visuais imersivos. meu projeto demonstra práticas 
de desenvolvimento e segurança da informação em um contexto empresarial
para fins de aprendizagem.

"Tive o pensamento de criar o CyFi, como um sistema de autenticação de
segurança de acesso, que combina verificações de identidade múltiplas 
com CAPTCHAs personalizados. O projeto implementa segurança em camadas, 
auditoria completa de feedback de acesso e uma experiência de usuário 
imersiva com efeitos visuais temáticos, demonstrando minha capacidade
de criar soluções robustas em C# como treinamento."


=======================================================================


§ Funcionalidades


1. Autenticação de Múltiplos Fatores

§ Nome completo + ID de funcionário + Senha

§ Sistema de bloqueio após 3 tentativas falhas

§ Hierarquia de usuários (Comum vs Chave Mestra)



2. Sistema CAPTCHA Dinâmico


§ MathCaptcha: Operações matemáticas com ruído visual

§ TextCaptcha: Reconhecimento de texto distorcido

§ SequenceCaptcha: Sequências lógicas numéricas

§ ImageCaptcha: Identificação de padrões ASCII



3. Auditoria e Compliance


📊 Logs detalhados de todos os acessos

§ Métricas de tempo de resposta

§ Detecção de comportamento suspeito


4. Experiência Imersiva


§ Efeitos visuais de glitch (Ciano/Vermelho)

§ Feedback em tempo real

§ Protocolos de segurança temáticos


========================================================

Para testes:

//Acesso Mestre

Login:"Ast.Fonseca" 

Nome completo:"Astolpho Assunção da Fonseca"

ID:"AS001"

Senha:"AAF@789"

==============================

//Acesso Padrão #1

Login:"Carlos.Ara"

Nome Completo:"Carlos Araujo Dos Santos"

ID:"CS002"

Senha:"Senha@456"

============================

//Acesso Padrão #2

Login:"Marina.Fer"

Nome Completo:"Marina Ferreira De Oliveira"

ID:"MO003"

Senha:"Senha@789"

============================

//Acesso Padrão #3

Login:"Joao.prr"

Nome Completo:"João Carlos Pereira"

ID:"JC004"

Senha:"Senha@101"


=================================================
Update 15 de Outubro de 2025

- Adição de novos CAPTCHAs com ML.NET.
