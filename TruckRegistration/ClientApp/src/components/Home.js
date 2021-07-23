import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Olá!</h1>
        <p>Bem vindo ao aplicativo de amostra de cadastro de caminhões. Você pode:</p>
        <ul>
          <li>Visualizar os caminhões cadastrados.</li>
          <li>Atualizar as informações de um caminhão.</li>
          <li>Excluir um caminhão.</li>
          <li>Inserir um novo caminhão.</li>
        </ul>
        <a href="/trucks">Clique aqui para começar!</a>
      </div>
    );
  }
}
