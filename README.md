# SafePoint Security

Projeto de software em atendimento à disciplina de TTC-2 da Faculdade do Centro Leste - 2016/02.
O mesmo trata-se do desenvolvimento de uma ferramenta capaz de auxiliar os profissionais administradores de ambientes SharePoint, a controlar da melhor forma as atividades no gerenciamento de segurança no ambiente SharePoint.Com a adoção dessa solução espera-se notar uma redução significativa dos incidentes relacionados ao controle de segurança e uma expectativa de economia em relação aos custos com consultorias.

## Sumário

* [Instalação](#instalacao)
* [Protótipos](#prototipos)
* [Autores](#autores)
* [Orientação](#orientacao)

## <a name="instalacao"></a> Instalação

Comandos SharePoint Management Shell:

```sh
Add-SPSolution -LiteralPath <PATH WSP FILE>
```
```sh
Install-SPSolution -Identity SafePointSecurity.wsp -WebApplication <WEBAPPLICATION NAME> -GACDeployment
```
```sh
Enable-SPFeature -identity  31d859bc-a61d-4bfc-ba39-08c9982b8a72 -Force
```
```sh
Enable-SPFeature -identity d98ad088-a33b-4f2a-9548-cafac22c55a7 -Url <URL SITECOLLECTION> -Force
```

Interface Web:
* Adicione uma nova página na Biblioteca de Páginas(SitePages) de um Conjuntos de Sites(Site Collection);
* Edite a página recém-criada e clique em Inserir Web Part;
* Na categoria SafePoint, selecione a webpart SafePointSecurity;
* Nas proriedades da Web Part, navegue até a seção SafePoint - Configurações e preencha as proriedades solicitadas;


## <a name="prototipos"></a> Protótipos

![](Documentation/Prototipos/Relatorio_Filtros.png)

![](Documentation/Prototipos/Relatorio_Resultado.png)

![](Documentation/Prototipos/Administracao_Grupos.png)

![](Documentation/Prototipos/Administracao_Grupos_AdicionarUsuario.png)

![](Documentation/Prototipos/Administracao_Permissoes.png)

![](Documentation/Prototipos/Administracao_Permissoes_AdicionarPermissao.png)

## <a name="autores"></a> Autores

* **Wilhas Mendes de Souza** - *Desenvolvedor/Analista de Requisitos* - <wilhsms@gmail.com>.
* **Zaíre Pianzola Pasti** - *Desenvolvedor/Analista de Requisitos* - <zaireppasti@gmail.com>.


## <a name="orientacao"></a> Orientação

* **André Ribeiro** - *Professor e Coordenador do curso de Sistemas de Informação* - <andrers@ucl.br>.
