import React, { Component } from 'react';
import { Button, Form, FormGroup, FormText, Input, Label } from 'reactstrap';

export class AddTruck extends Component {
  static displayName = AddTruck.name;

  constructor(props) {
    super(props);
    this.state = { success: false, model: '', productionYear: '', modelYear: '', error: {} };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(state) {
    this.setState(state);
  }

  handleSubmit() {
    this.saveTruck(this.state)
      .then(() => this.setState({ success: true }))
      .catch(() => this.setState({ success: false }));
  }

  renderForm(error) {
    return (
      <Form>
        <FormGroup>
          <Label for="model">Modelo</Label>
          <Input type="text" name="model" id="model" placeholder="Modelo (Poderá aceitar apenas FH e FM)"
            onChange={(e) => this.handleChange({ model: e.target.value })} />
          <FormText color="danger">
            {error.model}
          </FormText>
        </FormGroup>
        <FormGroup>
          <Label for="productionYear">Ano de Fabricação</Label>
          <Input type="number" name="productionYear" id="productionYear" placeholder="Ano de Fabricação (Ano deverá ser o atual)"
            onChange={(e) => this.handleChange({ productionYear: e.target.value })} />
          <FormText color="danger">
            {error.productionYear}
          </FormText>
        </FormGroup>
        <FormGroup>
          <Label for="modelYear">Ano Modelo</Label>
          <Input type="number" name="modelYear" id="modelYear" placeholder="Ano Modelo (Poderá ser o atual ou o ano subsequente)"
            onChange={(e) => this.handleChange({ modelYear: e.target.value })} />
          <FormText color="danger">
            {error.modelYear}
          </FormText>
        </FormGroup>
        <Button onClick={this.handleSubmit}>Salvar</Button>
      </Form>
    );
  }

  render() {
    let contents = this.state.success
      ? <p className="text-success"><em>Salvo!</em></p>
      : this.renderForm(this.state.error);

    return (
      <div>
        <h1 id="tabelLabel" >Adicionar caminhão</h1>
        {contents}
      </div>
    );
  }

  async saveTruck(state) {
    const model = {
      model: state.model,
      productionYear: state.productionYear,
      modelYear: state.modelYear
    };
    return await fetch('api/truck', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(model)
    });
  }
}
