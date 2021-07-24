import React, { Component } from 'react';
import { Card, CardBody, CardTitle, CardText, Col, Row } from 'reactstrap';

export class FetchTrucks extends Component {
  static displayName = FetchTrucks.name;

  constructor(props) {
    super(props);
    this.state = { trucks: [], loading: true };
  }

  componentDidMount() {
    this.populateTrucks();
  }

  static renderTrucks(trucks) {
    return (
      <Row>
        {trucks.map(truck =>
          <Col key={truck.id} sm="4">
            <div className="mt-4">
              <Card>
                <CardBody>
                  <CardTitle tag="h5">{truck.id} • {truck.model}</CardTitle>
                  <CardText>Produzido em {truck.productionYear}</CardText>
                  <CardText>Modelo {truck.modelYear}</CardText>
                </CardBody>
              </Card>
            </div>
          </Col>
        )}
      </Row>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Carregando...</em></p>
      : FetchTrucks.renderTrucks(this.state.trucks);

    return (
      <div>
        <h1 id="tabelLabel" >Lista de caminhões</h1>
        <p>Receber a lista de caminhões da API.</p>
        <a href="/add-truck">Adicionar caminhão</a>
        {contents}
      </div>
    );
  }

  async populateTrucks() {
    const response = await fetch('api/truck');
    const trucks = await response.json();
    this.setState({ trucks, loading: false });
  }
}
